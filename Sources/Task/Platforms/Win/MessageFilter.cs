using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Drawing;

namespace WebMarco
{
    //Creates a  message filter.
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class MyMessageFilter : IMessageFilter
    {
        //MainForm formOwner;


        //private bool canDragging = false;
        //private bool wasDragging = false;
        //private Point dragCursorPoint;
        //private Point dragFormPoint;
        //private Timer dragTimer;

        //private TimeSpan dragInterval = new TimeSpan(0, 0, 0, 0, 300);

        public MyMessageFilter()//MainForm aOwner)
        {
            //formOwner = aOwner;
           // dragTimer = new Timer();
            //dragTimer.Tick += new EventHandler(dragTimer_Tick);
            //dragTimer.Interval = 400;//TODO: from config
        }

        //void dragTimer_Tick(object sender, EventArgs e)
        //{
        //   // canDragging = true;
        //    //dragCursorPoint = Cursor.Position;
        //    //dragFormPoint = formOwner.Location;
        //}

        public bool PreFilterMessage(ref Message m)
        {
            //Blocks all the messages relating to the left mouse button. 
            if (m.Msg >= 512 || m.Msg <= 514)
            {
                //ApplicationHelper.WriteLineToDebug("Processing the messages : " + m.Msg);

                if (m.Msg == 513) //mouse click down
                {
                    //ApplicationHelper.WriteLineToDebug("Cursor : " + Cursor.Current.ToString());
                    //FOwner.Cursor.Equals
                    //dragTimer.Start();//TODO: drag goes here
                    //if (Cursor.Current != Cursors.Hand)
                    //MainForm.Instance.webBrowser.Enabled = false;
                }
                else if (m.Msg == 512) //mouse move
                {
                    //if (canDragging)
                    //{
                    //    //Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    //    //formOwner.Location = Point.Add(dragFormPoint, new Size(dif));
                    //    //wasDragging = true;
                    //    //return true;
                    //}
                    //else
                    //{
                    //    //canDragging = false;
                    //    //dragTimer.Stop();
                    //}
                    //MainForm.Instance.webBrowser.Enabled = false;
                }
                else if (m.Msg == 514) //mouse click up
                {
                    //canDragging = false;
                    //dragTimer.Stop();
                    //if (wasDragging)
                    //{
                    //    wasDragging = false;
                    //    return true;
                    //}

                    Timer releaseFormTimer = new Timer();
                    releaseFormTimer.Interval = 300;
                    releaseFormTimer.Tick += delegate
                    {
                        //MainForm.Instance.webBrowser.Enabled = true;
                        //MainForm.Instance.webBrowser.Focus();
                        releaseFormTimer.Stop();
                    };
                    releaseFormTimer.Start();

                }
                //return true;
            }
            return false;
        }
    }
}
