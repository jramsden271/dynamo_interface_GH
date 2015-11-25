using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;

namespace SBAFile._1b_SBA_tidy
{
    public class CustomAttributes2 : GH_ComponentAttributes
    {
        int titleheight = -1;
        int titlemargin = 5;
        int centrewidth = 100;
        int inputwidth = -1;
        int outputwidth = -1;
        int paramheight = -1;
        int bottommargin = 5;
        int parammargin = 2;

        int componentheight = -1;
        int componentwidth = -1;
        PointF componentpivot = new PointF();

        int maxnoofinputs = -1;

        Color bgcolour = Color.FromArgb(255, 203, 198, 190);
        Color bgcolour_selected = Color.FromArgb(255, 139, 224, 59);
        Color bgcolour_working = Color.FromArgb(255, 50, 50, 150);
        Color paramfontcolour = Color.FromArgb(255, 124, 124, 124);
        Color titlebgcolour = Color.FromArgb(255, 94, 92, 89);
        Color parambgcolour = Color.FromArgb(255, 228, 225, 221);

        Font paramfont = new Font("ubuntu", 9f);
        Font titlefont = new Font("ubuntu", 13f);
        Font messagefont = new Font("ubuntu", 8.5f);

        RectangleF componentBox;
        RectangleF titleBox;

        public CustomAttributes2(IGH_Component owner)
            : base(owner)
        {
        }

        protected override void Layout()
        {
            //base.Layout();
            
            //calc param width

            foreach (var param in Owner.Params.Input)
            {
                inputwidth = Math.Max(inputwidth, GH_FontServer.MeasureString(param.Name, paramfont).Width);
                paramheight = Math.Max(paramheight, GH_FontServer.MeasureString(param.Name, paramfont).Height);
            }
            foreach (var param in Owner.Params.Output)
            {
                outputwidth = Math.Max(outputwidth, GH_FontServer.MeasureString(param.Name, paramfont).Width);
                paramheight = Math.Max(paramheight, GH_FontServer.MeasureString(param.Name, paramfont).Height);
            }



            Pivot = GH_Convert.ToPoint(Pivot);
            //this.Bounds = new RectangleF(this.Pivot, new SizeF((float)(componentwidth + inputwidth + outputwidth), (float)(titleheight + titlemargin + bottommargin + (Math.Max(Owner.Params.Input.Count * paramheight, Owner.Params.Output.Count * paramheight)))));
            centrewidth = Math.Max(centrewidth, GH_FontServer.MeasureString(Owner.Message, messagefont).Width + 5);
            Size sizeedit = new Size(centrewidth, (2 * parammargin + paramheight) * Math.Max(Owner.Params.Input.Count, Owner.Params.Output.Count));
            componentBox = new RectangleF(Pivot, sizeedit);            
            
            LayoutInputParamsJR(base.Owner, componentBox);
            GH_ComponentAttributes.LayoutOutputParams(base.Owner, componentBox);

            componentpivot = new PointF(componentBox.Location.X, componentBox.Location.Y);
            componentwidth = centrewidth;
            int fudge = 3; //accounts for the fact that GH adds a small margin when creating parameter attributes
            if (Owner.Params.Input.Count > 0)
            {
                componentwidth += (int)(Owner.Params.Input[0].Attributes.Bounds.Width + fudge);
                componentpivot.X -= (Owner.Params.Input[0].Attributes.Bounds.Width + fudge);
            }
            if (Owner.Params.Output.Count > 0)
            {
                componentwidth += (int)(Owner.Params.Output[0].Attributes.Bounds.Width + fudge);
            }
            componentpivot.Y -= (titleheight + titlemargin);
            titleheight = GH_FontServer.MeasureString(Owner.Name, titlefont).Height;
            componentheight = (int)componentBox.Height + titleheight + titlemargin + bottommargin;


            titleBox = new RectangleF(componentpivot, new SizeF(componentwidth, titleheight));
            Bounds = new RectangleF(componentpivot, new SizeF(componentwidth, componentheight));
            

        }


        public override void ExpireLayout()
        {
            base.ExpireLayout();
        }





        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            
            if (channel == GH_CanvasChannel.Wires)
            {
                base.Render(canvas, graphics, channel);
                Layout();
            }
            if (channel == GH_CanvasChannel.Objects)
            {
                //debug
                /*
                //base.Render(canvas, graphics, channel);
                graphics.DrawRectangle(new Pen(Color.Yellow, 3f), GH_Convert.ToRectangle(Bounds));

                

                graphics.DrawRectangle(new Pen(Color.Blue, 2f), GH_Convert.ToRectangle(componentBox));

                foreach (var param in Owner.Params.Input)
                {
                    graphics.DrawRectangle(new Pen(Color.Green, 2f), GH_Convert.ToRectangle(param.Attributes.Bounds));
                }
                foreach (var param in Owner.Params.Output)
                {
                    graphics.DrawRectangle(new Pen(Color.Green, 2f), GH_Convert.ToRectangle(param.Attributes.Bounds));
                }

                graphics.DrawRectangle(new Pen(Color.Red, 2f), new Rectangle((int)(Bounds.X), (int)(Bounds.Y), (int)(Bounds.Width), (int)(titleheight)));
                */

                //background
                if (Owner.Attributes.Selected) graphics.FillRectangle(new SolidBrush(bgcolour_selected), Bounds);
                else graphics.FillRectangle(new SolidBrush(bgcolour), Bounds);

                //title
                graphics.FillRectangle(new SolidBrush(titlebgcolour), titleBox);
                float titlewidth = GH_FontServer.StringWidth(Owner.Name, titlefont);
                graphics.DrawString(Owner.Name, titlefont, Brushes.White, new PointF((float)(titleBox.Left + 0.5 * titleBox.Width - 0.5 * titlewidth), titleBox.Top));

                //params
                foreach (var param in Owner.Params.Input)
                {
                    graphics.FillRectangle(new SolidBrush(parambgcolour), param.Attributes.Bounds);
                    graphics.DrawString(param.Name, paramfont, new SolidBrush(paramfontcolour), new PointF(param.Attributes.Bounds.X, param.Attributes.Bounds.Y + parammargin));
                }
                //params
                foreach (var param in Owner.Params.Output)
                {
                    graphics.FillRectangle(new SolidBrush(parambgcolour), param.Attributes.Bounds);
                    int textwidth = GH_FontServer.StringWidth(param.Name, paramfont);
                    graphics.DrawString(param.Name, paramfont, new SolidBrush(paramfontcolour), new PointF(param.Attributes.Bounds.X, param.Attributes.Bounds.Y + parammargin));
                }

                //message
                graphics.DrawString(Owner.Message, messagefont, Brushes.Gray, componentBox.Location);

            }
        }







        // Grasshopper.Kernel.Attributes.GH_ComponentAttributes
        public void LayoutInputParamsJR(IGH_Component owner, RectangleF componentBox)
        {
            int count = owner.Params.Input.Count;
            if (count == 0)
            {
                return;
            }
            int width = 0;
            try
            {
                System.Collections.Generic.List<IGH_Param>.Enumerator enumerator = owner.Params.Input.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    IGH_Param param = enumerator.Current;
                    width = Math.Max(width, GH_FontServer.StringWidth(param.Name, paramfont));
                }
            }
            finally
            {
            }
            width = Math.Max(width + 6, 12);
            float pixelsPerParam = componentBox.Height / count;
            int arg_82_0 = 0;
            int num = count - 1;
            for (int i = arg_82_0; i <= num; i++)
            {
                IGH_Param param2 = owner.Params.Input[i];
                if (param2.Attributes == null)
                {
                    param2.Attributes = new GH_LinkedParamAttributes(param2, owner.Attributes);
                }
                float X = componentBox.X - width;
                float Y = componentBox.Y + i * pixelsPerParam;
                float W = width - 3;
                float H = pixelsPerParam;
                IGH_Attributes arg_103_0 = param2.Attributes;
                PointF pivot = new PointF(X + 0.5f * width, Y + 0.5f * pixelsPerParam);
                arg_103_0.Pivot = pivot;
                IGH_Attributes arg_12A_0 = param2.Attributes;
                RectangleF @in = new RectangleF(X, Y, W, H);
                arg_12A_0.Bounds = GH_Convert.ToRectangle(@in);
            }
        }
    }
}
