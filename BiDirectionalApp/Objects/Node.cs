using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.OpenGLES;
using MonoTouch.CoreAnimation;
namespace BiDirectionalApp
{
	public class Node : UIView
	{
		UILabel titleLabel;
		public String name;

		public Node () {
			this.Frame = new RectangleF(0, 0, 50, 50);

			this.BackgroundColor = UIColor.LightGray;
			this.Layer.CornerRadius = this.Bounds.Width / 2;
			this.Layer.MasksToBounds = true;

			titleLabel = new UILabel (new RectangleF(10,8,30,15));
			titleLabel.Text ="Label";
			titleLabel.TextAlignment = UITextAlignment.Center;
			titleLabel.Font = UIFont.FromName("Helvetica-Bold", 20f);
			this.Add(titleLabel);

		}
		public void SetTitleLabel(string data) {
			titleLabel.Text = data;
			name = data;
		}
		public void SetWeightLabel(string data) {
			//weightLabel.Text = data;
		}
	}
}

