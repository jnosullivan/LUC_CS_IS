// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace BiDirectionalApp
{
	[Register ("RootViewController")]
	partial class RootViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton next { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField TextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField a { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField b { get; set; }

		[Action ("add:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void add (UIBarButtonItem sender);

		[Action ("diss:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void diss (UITextField sender);

		[Action ("thenext:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void thenext (UIBarButtonItem sender);

		[Action ("UIButton204_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton204_TouchUpInside (UIButton sender);

		[Action ("UIButton205_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton205_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
