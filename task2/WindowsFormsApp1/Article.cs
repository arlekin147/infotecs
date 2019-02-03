using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Article
    {
        public Article() {
            panel = new FlowLayoutPanel();
            this.panel.FlowDirection = FlowDirection.TopDown;
            this.description = new System.Windows.Forms.Label();
            this.pubDate = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.LinkLabel();
            // 
            // label1
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(3, 0);
            this.description.Size = new System.Drawing.Size(35, (int)(Form1.layoutHight));
            this.description.TabIndex = 0;
            //
            //
            //
            this.pubDate.AutoSize = true;
            this.pubDate.Location = new System.Drawing.Point(3, 0);
            this.pubDate.Size = new System.Drawing.Size(35, (int)(Form1.layoutHight));
            this.pubDate.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(44, 0);
            this.title.Size = new System.Drawing.Size(55, (int)(Form1.layoutHight*0.2));
            this.title.TabIndex = 1;
            this.title.TabStop = true;
            this.title.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);


            this.panel.FlowDirection = FlowDirection.TopDown;
          
            this.description.Padding = new Padding(5);
            this.panel.Controls.Add(this.title);
            this.panel.Controls.Add(this.description);
            this.panel.Controls.Add(this.pubDate);
            this.panel.AutoSize = true;
        }

        public void setSize(int x, int y)
        {
            this.panel.Size = new System.Drawing.Size(x, y);
            this.description.Size = new System.Drawing.Size(x, (int)(y));
            this.pubDate.Size = new System.Drawing.Size(x, (int)(y));
            this.title.Size = new System.Drawing.Size(x, (int)(y));
        }

        public void add(Panel container)
        {

            container.Controls.Add(this.panel);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.Link);
        }

        private FlowLayoutPanel panel;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.LinkLabel title;
        private System.Windows.Forms.Label pubDate;


        public bool visibleOfTitle {
            get { return this.title.Visible; }
            set { this.title.Visible = value; }
        }
        public bool visibleOfDescription
        {
            get { return this.description.Visible; }
            set { this.description.Visible = value; }
        }
        public bool visibleOfPubDate
        {
            get { return this.pubDate.Visible; }
            set { this.pubDate.Visible = value; }
        }
        public System.Windows.Forms.Label Description
        {
            get
            {
                return this.description;
            }
        }
        public System.Windows.Forms.Label PubDate
        {
            get
            {
                return this.pubDate;
            }
        }
        public System.Windows.Forms.LinkLabel Title
        {
            get
            {
                return this.title;
            }

        }
        public string Link { get; set; }
    }

}
