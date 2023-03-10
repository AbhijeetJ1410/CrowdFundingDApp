using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nethereum.Hex.HexTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using Nethereum.Web3.Accounts;
using System.Numerics;
using Nethereum.Model;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.ABI.Encoders;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace CrowdsourceFundingDApp
{
    public partial class Form1 : Form
    {

        string erc20TokenContractAddress = ConfigurationManager.AppSettings["ERC20TokenAddress"];
        string smartContractAddress = ConfigurationManager.AppSettings["CrowdFundSmartContract"];
        string user1Add = ConfigurationManager.AppSettings["Account1Address"];
        string user2Add = ConfigurationManager.AppSettings["Account2Address"];
        string user3Add = ConfigurationManager.AppSettings["Account3Address"];
        string user4Add = ConfigurationManager.AppSettings["Account4Address"];

        string _privateKey = string.Empty;

        Web3 _web3;

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        
        public Form1()
        {
            InitializeComponent();
            // Initialize web3 with the Infura endpoint

            _web3 = new Web3(ConfigurationManager.AppSettings["InfuraAPIKey"]);

            PopulateAddress();
            LoadERCTokenInformation();
            UpdateBalances();

            cmbSelectSender.Items.Add(lblUserAccount1.Text);
            cmbSelectSender.Items.Add(lblUserAccount2.Text);
            cmbSelectSender.Items.Add(lblUserAccount3.Text);
            cmbSelectSender.Items.Add(lblUserAccount4.Text);

            keyValuePairs.Add(lblUserAccount1.Text, lblAddress1.Text);
            keyValuePairs.Add(lblUserAccount2.Text, lblAddress2.Text);
            keyValuePairs.Add(lblUserAccount3.Text, lblAddress3.Text);
            keyValuePairs.Add(lblUserAccount4.Text, lblAddress4.Text);
        }

        public void PopulateAddress()
        {
            lblAddress1.Text = ConfigurationManager.AppSettings["Account1Address"];
            lblAddress2.Text = ConfigurationManager.AppSettings["Account2Address"];
            lblAddress3.Text = ConfigurationManager.AppSettings["Account3Address"];
            lblAddress4.Text = ConfigurationManager.AppSettings["Account4Address"];
            lblSmartContractAddress.Text = ConfigurationManager.AppSettings["CrowdFundSmartContract"];
        }

        public async void LoadERCTokenInformation()
        {
            string erc20TokenContractAddress = ConfigurationManager.AppSettings["ERC20TokenAddress"];

            //ulong balance = await GetBalance(/*_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf"*/);
            //lblBalance.Text = balance.ToString();

            string name = await ExecuteCallData<string>(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "name");
            lblTokenName.Text = name.ToString();

            ulong totalSupply = await ExecuteCallData<ulong>(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "totalSupply");
            lblTotalSupply.Text = totalSupply.ToString();

            string symbol = await ExecuteCallData<string>(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "symbol");
            lblSymbol.Text = symbol.ToString();

            uint dec = await ExecuteCallData<uint>(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "decimals");
            lblDecimal.Text = dec.ToString();

            //uint fundingGoal = await GetFundingGoal(_web3, erc20TokenContractAddress, Constants.SmartContractABI, "fundingGoal");
            //lblFundingGoal.Text = fundingGoal.ToString();

        }

        public async void UpdateBalances()
        {
            ulong balance = await GetBalance(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf", user1Add);
            lblBalUser1.Text = balance.ToString();

            ulong balance1 = await GetBalance(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf", user2Add);
            lblBalUser2.Text = balance1.ToString();

            ulong balance2 = await GetBalance(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf", user3Add);
            lblBalUser3.Text = balance2.ToString();

            ulong balance3 = await GetBalance(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf", user4Add);
            lblBalUser4.Text = balance3.ToString();

            ulong balance4 = await GetBalance(_web3, erc20TokenContractAddress, Constants.MyTokenABI, "balanceOf", smartContractAddress);
            lblBalSmartContract.Text = balance4.ToString();
        }

        private void txtContractAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private async Task<ulong> GetBalance(Web3 web3, string erc20ContractAddress, string contractAbi, string functionName, string smartContractAddress)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, erc20ContractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<ulong>(smartContractAddress);

            return result;


        }

        private async Task<ulong> GetTotalSupply(Web3 web3, string contractAddress, string contractAbi, string functionName)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<ulong>();

            return result;
        }

        private async Task<uint> GetFundingGoal(Web3 web3, string contractAddress, string contractAbi, string functionName)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);
            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<uint>();

            return result;
        }

        private async Task<string> GetTokenName(Web3 web3, string contractAddress, string contractAbi, string functionName)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<string>();

            return result;
        }

        private async Task<string> GetSymbol(Web3 web3, string contractAddress, string contractAbi, string functionName)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<string>();

            return result;
        }

        private async Task<uint> GetDecimals(Web3 web3, string contractAddress, string contractAbi, string functionName)
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<uint>();

            return result;
        }

        private async Task Approve(string contractAddress, string contractAbi, string functionName, params object[] parameters)
        {
            await ExecuteTransaction(contractAddress, contractAbi, functionName, parameters);
        }


        private async Task<T> ExecuteCallData<T>(Web3 web3, string contractAddress, string contractAbi, string functionName) 
        {
            var contract = new Contract((Nethereum.RPC.EthApiService)web3.Eth, contractAbi, contractAddress);

            // Call a contract function
            var function = contract.GetFunction(functionName);
            var result = await function.CallAsync<T>();

            return result;
        }
        private async Task ExecuteTransaction(string contractAddress, string contractAbi, string functionName, params object[] parameters)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["InfuraAPIKey"];
                var account = new Nethereum.Web3.Accounts.Account(_privateKey);
                var newWeb3 = new Web3(account, url);

                var senderAddress = account.Address;

                var contract = newWeb3.Eth.GetContract(contractAbi, contractAddress);

                //Using the contract we can retrieve the functions using their name.
                var function = contract.GetFunction(functionName);

                TransactionReceipt receipt = await function.SendTransactionAndWaitForReceiptAsync(senderAddress, new HexBigInteger(3000000), null, null, parameters);

                if (receipt != null)
                {
                    if (receipt.Status.Value == new BigInteger(1))
                    {
                        MessageBox.Show("Transaction succeeded");
                        var logs = receipt.HasLogs() ? receipt.Logs : null;
                        if (logs != null && logs.Count > 0)
                        {
                            MyMessageBox.Show(MyMessageBox.MessageType.Information, receipt.TransactionHash, logs.ToString());
                        }
                    }
                    else
                    {
                        bool? errors = receipt.HasErrors();
                        if (errors != null && errors == true)
                        {
                            MyMessageBox.Show(MyMessageBox.MessageType.Error, receipt.TransactionHash);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception is " + ex.ToString());
            }
        }

        private async void Pledge(string contractAddress, string contractAbi, string functionName, params object[] parameters)
        {
            await ExecuteTransaction(contractAddress, contractAbi, functionName, parameters);
        }

        private async void Withdraw(string contractAddress, string contractAbi, string functionName)
        {
            await ExecuteTransaction(contractAddress, contractAbi, functionName, null);
        }

        private async void ClaimRefund(string contractAddress, string contractAbi, string functionName)
        {
            await ExecuteTransaction(contractAddress, contractAbi, functionName, null);
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {		
            if (cmbSelectSender.SelectedItem != null && txtAmount.Text.Trim() != null)
            {
                //string selectedAddress = keyValuePairs[cmbSelectSender.SelectedItem.ToString()];
                int amount = int.Parse(txtAmount.Text.Trim());
                object[] parameters = 
                {
                    amount
                }; 
                await Approve(erc20TokenContractAddress, Constants.MyTokenABI, "approve", parameters);
            }
            else
            {
                MessageBox.Show("Please select Sender and enter amount");
            }
        }

        private void btnPledge_Click(object sender, EventArgs e)
        {
            if (cmbSelectSender.SelectedItem != null && txtAmount.Text.Trim() != string.Empty)
            {
                int amount = int.Parse(txtAmount.Text.Trim());
                object[] parameters =
                {
                    amount
                };
                Pledge(smartContractAddress, Constants.SmartContractABI, "pledge", parameters);
            }
            else
            {
                MessageBox.Show("Please select Sender and enter amount");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (cmbSelectSender.SelectedItem != null)
            {
                Withdraw(smartContractAddress, Constants.SmartContractABI, "withdraw");
            }
            else
            {
                MessageBox.Show("Please select Sender and enter amount");
            }
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (cmbSelectSender.SelectedItem != null)
            {
                ClaimRefund(smartContractAddress, Constants.SmartContractABI, "claimRefund");
            }
            else
            {
                MessageBox.Show("Please select Sender and enter amount");
            }
        }

        private void cmbSelectSender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = cmbSelectSender.SelectedItem.ToString();

            if (text == lblUserAccount1.Text.Trim())
                _privateKey = ConfigurationManager.AppSettings["Account1PrivateKey"];
            else if (text == lblUserAccount2.Text.Trim())
                _privateKey = ConfigurationManager.AppSettings["Account2PrivateKey"];
            else if (text == lblUserAccount3.Text.Trim())
                _privateKey = ConfigurationManager.AppSettings["Account3PrivateKey"];
            else if (text == lblUserAccount4.Text.Trim())
                _privateKey = ConfigurationManager.AppSettings["Account4PrivateKey"];
            else
                _privateKey = string.Empty;
        }

        private void btnUpdateBalances_Click(object sender, EventArgs e)
        {
            UpdateBalances();
        }
    }
}
