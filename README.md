# Crowdfunding using Solidity and DAPP - ERC20 Tokens

This project provides two smart contracts, one that implements a ERC20 token and another that allows users to pledge and claim their tokens. This project includes frontend designed in .NET for interacting with these contracts.

## Requirements : Create a crowdfunding campaign where users can pledge funds to and claim funds from the contract.

1. Funds take the form of ERC20 token.
2. Crowdfunding projects have a funding goal.
3. When a funding goal is not met, then customers are able to get a refund of their pledged funds.
4. dApps using the contract can observe state changes.
5. Optional Bonus: Contract is upgradable.

## Pre- requisites : Before Executing the application ensure you have all the information mentioned below.
1.	Create 4 user accounts on Ethereum blockchain using Metask wallet. Note the account Address and private key.
2.	Every account should have enough ethers to execute the transaction.
3.	Deploy the ERC 20 token smart contract and Token Deposit smart contract on sepolia test net. We have used Sepolia test network
4.	Once the contract is deployed, the account used for deployment will have the ERC20 tokens. 
5.	Distribute the ERC20 tokens to other accounts and provide approval for token transfer.
6.  Project has app.config file. All the the contract address and account address details in the app.config.
<appSettings>
    <!-- ENTER INFURE API KEY -->
    <add key="InfuraAPIKey" value="" />
    <!-- ENTER ADDRESS FOR ACCOUNT1 -->
    <add key="Account1Address" value="" />
    <!-- ENTER ADDRESS FOR ACCOUNT2 -->
    <add key="Account2Address" value="" />
    <!-- ENTER ADDRESS FOR ACCOUNT3 -->
    <add key="Account3Address" value="" />
    <!-- ENTER ADDRESS FOR ACCOUNT4 -->
    <add key="Account4Address" value="" />

    <!-- ENTER PRIVATE KEY FOF ACCOUNT1 -->
    <add key="Account1PrivateKey" value="" />
    <!-- ENTER PRIVATE KEY FOF ACCOUNT2 -->
    <add key="Account2PrivateKey" value="" />
    <!-- ENTER PRIVATE KEY FOF ACCOUNT3 -->
    <add key="Account3PrivateKey" value="" />
    <!-- ENTER PRIVATE KEY FOF ACCOUNT4 -->
    <add key="Account4PrivateKey" value="" />

    <!-- ENTER ERC20 TOKENS SMART CONTRACT ADDRESS -->
    <add key="ERC20TokenAddress" value="" />
    <!-- ENTER TOKEN DEPOSIT SMART CONTRACT ADDRESS -->
    <add key="CrowdFundSmartContract" value="" />
  </appSettings>
  7.  Build the project in release configuration and execute.
