using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CrowdsourceFundingDApp
{
    public class MyMessageBox : Form
    {
        private static MyMessageBox _box = null;
        private TextBox txtTransactionDetails;
        private Label label1;
        private Label label2;
        private Button btnOk;

        public enum MessageType
        {
            Error,
            Information
        }

        private MyMessageBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtTransactionDetails = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTransactionDetails
            // 
            this.txtTransactionDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTransactionDetails.Location = new System.Drawing.Point(12, 79);
            this.txtTransactionDetails.Multiline = true;
            this.txtTransactionDetails.Name = "txtTransactionDetails";
            this.txtTransactionDetails.Size = new System.Drawing.Size(516, 197);
            this.txtTransactionDetails.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(191, 292);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(129, 35);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Transaction details are available in etherscan.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please visit below url for details.";
            // 
            // MyMessageBox
            // 
            this.ClientSize = new System.Drawing.Size(541, 339);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtTransactionDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            _box.Dispose();
            _box = null;
        }

        public static new void Show(MessageType messageType, string transactionHash, string logs = null)
        {
            if(_box == null)
            {
                _box = new MyMessageBox();
                _box.txtTransactionDetails.Text += "Transaction Details :" + Environment.NewLine;
                _box.txtTransactionDetails.Text = "https://sepolia.etherscan.io/tx/" + transactionHash;

                if(!string.IsNullOrEmpty(logs) ) {

                    _box.txtTransactionDetails.Text += Environment.NewLine;
                    _box.txtTransactionDetails.Text += Environment.NewLine;
                    _box.txtTransactionDetails.Text += "--------------------------------------------------" + Environment.NewLine;
                    _box.txtTransactionDetails.Text += "Log Details :" + Environment.NewLine + logs;
                }

                if(messageType == MessageType.Error)
                {
                    _box.Text = "ERROR OCCURRED";
                }
                _box.ShowDialog();
            }
        }
    }
}
