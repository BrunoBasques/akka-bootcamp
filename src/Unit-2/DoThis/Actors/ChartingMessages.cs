﻿using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace ChartApp.Actors
{
    #region Reporting

    /// <summary>
    /// Signal used to indicate that it's time to sample all counters.
    /// </summary>
    /// 
    public class GatherMetrics { }

    /// <summary>
    /// Metric data at the time of sample.
    /// </summary>
    public class Metric
    {
        public string Series { get; private set; }
        public float CounterValue { get; private set; }

        public Metric(string series, float counterValue)
        {
            Series = series;
            CounterValue = counterValue;
        }
    }
    #endregion

    #region Performance Counter Management

    /// <summary>
    /// All types of counters supported by this example.
    /// </summary>
    public enum CounterType
    { 
        Cpu,
        Memory,
        Disk
    }

    /// <summary>
    /// Enables a counter and begins publishing values to <see cref="Subscriber"/>.
    /// </summary>
    public class SubscribeCounter
    {
        public CounterType Counter { get; private set; }
        public IActorRef Subscriber { get; private set; }

        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Counter = counter;
            Subscriber = subscriber;
        }
    }

    /// <summary>
    /// Unsubscribes <see cref="Subscriber"/> from receiving updates for a given counter. 
    /// </summary>
    public class UnsubscribeCounter
    {
        public CounterType Counter { get; private set; }
        public IActorRef Subscriber { get; private set; }

        public UnsubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Counter = counter;
            Subscriber = subscriber;
        }
    }

    #endregion
}
