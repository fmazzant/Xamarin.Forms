﻿using System.ComponentModel;
using CoreGraphics;

namespace Xamarin.Forms.Platform.iOS
{
	public class PolygonRenderer : ShapeRenderer<Polygon, PolygonView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Polygon> args)
        {
            if (Control == null)
            {
                SetNativeControl(new PolygonView());
            }

            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                UpdatePoints();
                UpdateFillRule();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == Polygon.PointsProperty.PropertyName)
                UpdatePoints();
            else if (args.PropertyName == Polygon.FillRuleProperty.PropertyName)
                UpdateFillRule();
        }

        void UpdatePoints()
        {
            Control.UpdatePoints(Element.Points.ToCGPoints());
        }

        public void UpdateFillRule()
        {
            Control.UpdateFillMode(Element.FillRule == FillRule.Nonzero);
        }
    }

    public class PolygonView : ShapeView
    {
        public void UpdatePoints(CGPoint[] points)
        {
			var path = new CGPath();
            path.AddLines(points);
            path.CloseSubpath();

            ShapeLayer.UpdateShape(path);
        }

        public void UpdateFillMode(bool fillMode)
        {
            ShapeLayer.UpdateFillMode(fillMode);
        }
    }
}
