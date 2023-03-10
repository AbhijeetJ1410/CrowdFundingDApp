pragma solidity ^0.8.9;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract CustomToken is ERC20 {
    address private owner;

    constructor(uint256 initialSupply) ERC20("ADJCoin", "ADJ") {
        _mint(msg.sender, initialSupply);
        owner = msg.sender;
    }

    function Mint(address to, uint256 supply) external {
        require(msg.sender == owner, "Only Owner has right to mint.");
        _mint(to, supply);
    }
}
