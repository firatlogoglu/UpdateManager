using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using UpdateManager.Languages;

namespace UpdateManager
{
    public class UpdateManagerForm : Form
    {
        private Button btnInstallUpdates;
        private Label lblCurrentVersion;
        private Label lblNewVersion;
        private Label lblANewVersionAva;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Button btnDownloadUpdates;
        private LinkLabel lnkLblDirectDownload;
        private Button btnCheckUpdates;
        private LinkLabel lnkLblOpenFolder;

        private readonly string productName = Application.ProductName;
        private readonly string productVersion = Application.ProductVersion;
        private readonly bool is64BitProcess = Environment.Is64BitProcess;

        private string UrlGitHubReleases;
        private string urlDownload;
        private string UrlVersion;
        private string path;
        private double FileSize;
        private double Percentage;
        public UpdateManagerForm(string urlVersion, string urlGitHubReleases)
        {
            InitializeComponent();
            lblStatus.Text = "";
            UrlVersion = urlVersion;
            UrlGitHubReleases = urlGitHubReleases;
            Lang();
        }

        private void InitializeComponent()
        {
            this.btnInstallUpdates = new System.Windows.Forms.Button();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.lblANewVersionAva = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnDownloadUpdates = new System.Windows.Forms.Button();
            this.lnkLblDirectDownload = new System.Windows.Forms.LinkLabel();
            this.btnCheckUpdates = new System.Windows.Forms.Button();
            this.lnkLblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnInstallUpdates
            // 
            this.btnInstallUpdates.AutoSize = true;
            this.btnInstallUpdates.Enabled = false;
            this.btnInstallUpdates.Location = new System.Drawing.Point(12, 154);
            this.btnInstallUpdates.Name = "btnInstallUpdates";
            this.btnInstallUpdates.Size = new System.Drawing.Size(132, 23);
            this.btnInstallUpdates.TabIndex = 2;
            this.btnInstallUpdates.Text = "&Install Updates";
            this.btnInstallUpdates.UseVisualStyleBackColor = true;
            this.btnInstallUpdates.Click += new System.EventHandler(this.btnInstallUpdates_Click);
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(12, 46);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(82, 13);
            this.lblCurrentVersion.TabIndex = 1;
            this.lblCurrentVersion.Text = "Current Version:";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(12, 59);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(68, 13);
            this.lblNewVersion.TabIndex = 1;
            this.lblNewVersion.Text = "Last Version:";
            // 
            // lblANewVersionAva
            // 
            this.lblANewVersionAva.AutoSize = true;
            this.lblANewVersionAva.Location = new System.Drawing.Point(12, 9);
            this.lblANewVersionAva.Name = "lblANewVersionAva";
            this.lblANewVersionAva.Size = new System.Drawing.Size(136, 13);
            this.lblANewVersionAva.TabIndex = 2;
            this.lblANewVersionAva.Text = "A New Version is Available.";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 197);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(251, 23);
            this.progressBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 227);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status";
            // 
            // btnDownloadUpdates
            // 
            this.btnDownloadUpdates.AutoSize = true;
            this.btnDownloadUpdates.Enabled = false;
            this.btnDownloadUpdates.Location = new System.Drawing.Point(12, 120);
            this.btnDownloadUpdates.Name = "btnDownloadUpdates";
            this.btnDownloadUpdates.Size = new System.Drawing.Size(132, 23);
            this.btnDownloadUpdates.TabIndex = 1;
            this.btnDownloadUpdates.Text = "&Download Updates";
            this.btnDownloadUpdates.UseVisualStyleBackColor = true;
            this.btnDownloadUpdates.Click += new System.EventHandler(this.btnDownloadUpdates_Click);
            // 
            // lnkLblDirectDownload
            // 
            this.lnkLblDirectDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkLblDirectDownload.AutoSize = true;
            this.lnkLblDirectDownload.Enabled = false;
            this.lnkLblDirectDownload.Location = new System.Drawing.Point(175, 130);
            this.lnkLblDirectDownload.Name = "lnkLblDirectDownload";
            this.lnkLblDirectDownload.Size = new System.Drawing.Size(86, 13);
            this.lnkLblDirectDownload.TabIndex = 1;
            this.lnkLblDirectDownload.TabStop = true;
            this.lnkLblDirectDownload.Text = "&Direct Download";
            this.lnkLblDirectDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblDirectDownload_LinkClicked);
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.AutoSize = true;
            this.btnCheckUpdates.Location = new System.Drawing.Point(11, 91);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(135, 23);
            this.btnCheckUpdates.TabIndex = 0;
            this.btnCheckUpdates.Text = "&Check For Updates";
            this.btnCheckUpdates.UseVisualStyleBackColor = true;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // lnkLblOpenFolder
            // 
            this.lnkLblOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkLblOpenFolder.AutoSize = true;
            this.lnkLblOpenFolder.Location = new System.Drawing.Point(8, 249);
            this.lnkLblOpenFolder.Name = "lnkLblOpenFolder";
            this.lnkLblOpenFolder.Size = new System.Drawing.Size(74, 13);
            this.lnkLblOpenFolder.TabIndex = 3;
            this.lnkLblOpenFolder.TabStop = true;
            this.lnkLblOpenFolder.Text = "&Show in folder";
            this.lnkLblOpenFolder.Visible = false;
            this.lnkLblOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblOpenFolder_LinkClicked);
            // 
            // UpdateManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(273, 274);
            this.Controls.Add(this.lnkLblOpenFolder);
            this.Controls.Add(this.lnkLblDirectDownload);
            this.Controls.Add(this.btnCheckUpdates);
            this.Controls.Add(this.btnDownloadUpdates);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblANewVersionAva);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.btnInstallUpdates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateManagerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Manager";
            this.Load += new System.EventHandler(this.UpdateManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Lang()
        {
            this.Text = Localization.UpdateManager;

            lblNewVersion.Text = "";
            lblANewVersionAva.Text = "";
            btnCheckUpdates.Text = Localization.CheckUpdates;
            btnInstallUpdates.Text = Localization.InstallUpdates;
            btnDownloadUpdates.Text = Localization.DownloadUpdates;
            lnkLblDirectDownload.Text = Localization.DirectDownload;
            lblCurrentVersion.Text = Localization.CurrentVersion + " " + productVersion + "\r\n";
            lnkLblOpenFolder.Text = Localization.OpenFolder;
        }

        private void CheckUpdate()
        {
            try
            {
                btnCheckUpdates.Enabled = false;
                lnkLblDirectDownload.Enabled = false;
                bool ass = UpdateManager.Update.Check(productVersion, is64BitProcess, UrlVersion, UrlGitHubReleases + "download/", productName + ".Setup", Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads", out string newVersion, out urlDownload, out path);

                if (ass)
                {
                    btnDownloadUpdates.Enabled = true;
                    btnCheckUpdates.Enabled = true;
                    lnkLblDirectDownload.Enabled = true;
                    lblANewVersionAva.Text = Localization.lblANewVersionAva + "\n" + Localization.lblANewVersionAva2;
                    lblNewVersion.Text = Localization.LastVersion + " " + newVersion;
                }
                else
                {
                    btnCheckUpdates.Enabled = true;
                    btnInstallUpdates.Enabled = false;
                    btnDownloadUpdates.Enabled = false;
                    lblANewVersionAva.Text = Localization.NoNewVersion;
                    lnkLblDirectDownload.Enabled = true;
                    lblNewVersion.Text = Localization.LastVersion + " " + productVersion;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lblNewVersion.Text = Localization.LastVersion + " " + Localization.ERROR_2 + " " + Localization.LatestVersionNotDetected;
                lblANewVersionAva.Text = Localization.ERROR_2 + " " + Localization.LatestVersionNotDetected;
                lnkLblDirectDownload.Enabled = true;
                btnCheckUpdates.Enabled = true;
            }
        }

        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }

        private void btnDownloadUpdates_Click(object sender, EventArgs e)
        {
            btnCheckUpdates.Enabled = false;
            btnDownloadUpdates.Enabled = false;

            Download.DownloadUpdates(urlDownload, path, WebDownloadFileCompleted, WebDownloadProgressChanged);
        }

        private void WebDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Minimum = 0;
            double receive = double.Parse(e.BytesReceived.ToString());
            FileSize = double.Parse(e.TotalBytesToReceive.ToString());
            Percentage = receive / FileSize * 100;
            lblStatus.Text = Localization.Downloading + $" { string.Format("{0:0.##}%", Percentage)}";
            progressBar.Value = int.Parse(Math.Truncate(Percentage).ToString());
            progressBar.Update();
        }

        private void WebDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                btnDownloadUpdates.Enabled = true;
            }
            else
            {
                lblStatus.Text = Localization.Downloaded + " " + "100%" + " " + Localization.TotalSize + $" {string.Format("{0:0.##} KB", FileSize / Percentage)}";
                lnkLblOpenFolder.Text = path;
                lnkLblOpenFolder.Visible = true;
                btnDownloadUpdates.Enabled = false;
                btnInstallUpdates.Enabled = true;
            }
        }

        private void btnInstallUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                btnInstallUpdates.Enabled = false;
                Install.InstallUpdates(path, productName);
                Application.Restart();
            }
            catch (Exception ex)
            {
                btnInstallUpdates.Enabled = true;
                MessageBox.Show(ex.Message);
            }
        }

        private void lnkLblDirectDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Download.DirectDownload(UrlGitHubReleases + "latest", urlDownload);
                lnkLblDirectDownload.LinkVisited = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateManagerForm_Load(object sender, EventArgs e)
        {
            CheckUpdate();
        }

        private void lnkLblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkLblOpenFolder.LinkVisited = true;
            string cmd = "explorer.exe";
            string arg = "/select, " + path;
            System.Diagnostics.Process.Start(cmd, arg);
        }
    }
}