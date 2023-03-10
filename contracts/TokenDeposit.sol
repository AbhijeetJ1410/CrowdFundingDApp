pragma solidity ^0.8.9;

import "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "./CustomToken.sol";

contract TokenDeposit is Ownable {
    
    uint public fundingGoal; // Funding goal in Custom Token
    uint public totalPledged; // Total amount pledged in custom Token
    uint public deadline; //Number of days challenge will remain open.
    bool public goalMet; //Boolean flag indicating if the funding goal has been met
    mapping(address => uint) public pledges; // Mapping of addresses to pledged amounts
    
    CustomToken public token; //Custom token used for pledges
    
    //Events Declaration
    event GoalMet(uint totalPledged);
    event Pledged(address backer, uint amount);
    event Refunded(address backer, uint amount);
    
    //Constructor is called when the contract is deployed.
    constructor(address _token, uint _fundingGoal, uint _deadline) {
        token = CustomToken(_token);
        fundingGoal = _fundingGoal;
        deadline = block.timestamp + (_deadline * 1 days);
    }

    modifier campaignIsOpen(){
        require(block.timestamp <= deadline, "Crowdfunding campaign is closed.");
        _;
    }

    modifier campaignIsClosed(){
        require(block.timestamp > deadline, "Crowdfunding campaign is still open. Cannot Withdraw Funds.");
        _;
    }

    modifier fundingGoalNotReached(){
        require(!goalMet, "Goal has already been met");
        _;
    }

    function getFundingGoal() external view returns(uint){
        return fundingGoal;
    }

    //Allow users to pledge funds
    function pledge(uint amount) external campaignIsOpen fundingGoalNotReached {
        require(amount > 0, "Amount must be greater than zero");
        token.transferFrom(msg.sender, address(this), amount);
        pledges[msg.sender] += amount;
        totalPledged += amount;
        emit Pledged(msg.sender, amount);
        if (totalPledged >= fundingGoal) {
            goalMet = true;
            emit GoalMet(totalPledged);
        }
    }
    
    //Allow users to claim a refund if the funding goal is not met
    function claimRefund() external fundingGoalNotReached{
        require(!goalMet, "Goal has been met");
        require(pledges[msg.sender] > 0, "No pledge to refund");
        uint amount = pledges[msg.sender];
        pledges[msg.sender] = 0;
        totalPledged -= amount;
        token.transfer(msg.sender, amount);
        emit Refunded(msg.sender, amount);
    }
    
    //Allow the owner to withdraw the pledged funds if the funding goal is met
    //It destroys the contract and transfers the contract funds to the owner.
    function withdraw() external onlyOwner campaignIsClosed {
        require(goalMet, "Goal has not been met");
        uint amount = token.balanceOf(address(this));
        //Transfers the ERC20 token to the owner
        token.transfer(owner(), amount);
        //Apart from EC20 token, if there are any contract funds, 
        //those are added to the owner.
        selfdestruct(payable(owner()));
    }

    //Allow the owner to extend the deadline.
    function extendDeadline(uint numberOfDays) external onlyOwner {
        deadline += numberOfDays * 1 days;
    }

    //Allow the owner to extend the funding goal if required.
    function extendFundingGoal(uint amount) external onlyOwner {
        fundingGoal += amount;
        if(totalPledged < fundingGoal){
            goalMet = false;
        }
        else{
            goalMet = true;
            emit GoalMet(totalPledged);
        }
    }
}
