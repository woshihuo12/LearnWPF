using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mumu_extendedcontrol
{
	public enum ProcessStage
	{
        Stage0,
        Stage1,
		Stage2,
		Stage3,
		Stage4,
		Stage5
	}

	public class ProcessStageHelper : DependencyObject
	{
		public static readonly DependencyProperty ProcessCompletionProperty = DependencyProperty.RegisterAttached(
		"ProcessCompletion", typeof(double), typeof(ProcessStageHelper), new PropertyMetadata(0.0, OnProcessCompletionChanged));

		private static readonly DependencyPropertyKey ProcessStagePropertyKey = DependencyProperty.RegisterAttachedReadOnly(
        "ProcessStage", typeof(ProcessStage), typeof(ProcessStageHelper), new PropertyMetadata(ProcessStage.Stage0));

		public static readonly DependencyProperty ProcessStageProperty = ProcessStagePropertyKey.DependencyProperty;

		private static void OnProcessCompletionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			double progress = (double)e.NewValue;
			ProgressBar bar = d as ProgressBar;
            if (progress >= 0 && progress < 20) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage0);
			if (progress >= 20 && progress < 40) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage1);
			if (progress >= 40 && progress < 60) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage2);
			if (progress >= 60 && progress < 80) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage3);
			if (progress >= 80 && progress < 100) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage4);
			if (progress == 100) bar.SetValue(ProcessStagePropertyKey, ProcessStage.Stage5);
		}

		public static void SetProcessCompletion(ProgressBar bar, double progress)
		{
			bar.SetValue(ProcessCompletionProperty, progress);
		}
	
		public static void SetProcessStage(ProgressBar bar, ProcessStage stage)
		{
			bar.SetValue(ProcessStagePropertyKey, stage);
		}
	}
}