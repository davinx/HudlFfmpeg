﻿using System;
using System.Text;
using Hudl.FFmpeg.Attributes;
using Hudl.FFmpeg.BaseTypes;
using Hudl.FFmpeg.Common;
using Hudl.FFmpeg.Enums;
using Hudl.FFmpeg.Filters.Attributes;
using Hudl.FFmpeg.Filters.BaseTypes;
using Hudl.FFmpeg.Filters.Formatters;
using Hudl.FFmpeg.Filters.Interfaces;
using Hudl.FFmpeg.Resources.BaseTypes;

namespace Hudl.FFmpeg.Filters
{
    /// <summary>
    /// Video filter that applies a fade in or out effect.
    /// </summary>
    [ForStream(Type=typeof(VideoStream))]
    [Filter(Name = "fade", MinInputs = 1, MaxInputs = 1)]
    public class Fade : IFilter
    {
        public Fade()
        {
        }
        public Fade(double? startUnit, double? lengthInUnits, VideoUnitType unitType)
        {
            if (unitType == VideoUnitType.Frames)
            {
                StartFrame = startUnit;
                NumberOfFrames = lengthInUnits;
            }
            else
            {
                StartTime = startUnit;
                Duration = lengthInUnits; 
            }
        }

        public Fade(double? startUnit, double? lengthInUnits, VideoUnitType unitType, FadeTransitionType transitionType)
            : this(startUnit, lengthInUnits, unitType)
        {
            TransitionType = transitionType;
        }

        [FilterParameter(Name = "t", Default = FadeTransitionType.In, Formatter = typeof(EnumParameterFormatter))]
        public FadeTransitionType? TransitionType { get; set; }

        [FilterParameter(Name = "s")]
        [Validator(LogicalOperators.GreaterThan, 0)]
        public double? StartFrame { get; set; }

        [FilterParameter(Name = "s")]
        [Validator(LogicalOperators.GreaterThan, 0)]
        public double? NumberOfFrames { get; set; }

        [FilterParameter(Name = "st")]
        [Validator(LogicalOperators.GreaterThan, 0)]
        public double? StartTime { get; set; }

        [FilterParameter(Name = "sd")]
        [Validator(LogicalOperators.GreaterThan, 0)]
        public double? Duration { get; set; }

        [FilterParameter(Name = "alpha", Formatter = typeof(BoolToInt32Formatter))]
        public bool Alpha { get; set; }

        [FilterParameter(Name = "c")]
        public string Color { get; set; }
    }
}
