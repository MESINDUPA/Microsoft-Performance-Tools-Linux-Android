﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Microsoft.Performance.SDK;
using PerfettoProcessor;
using System.Collections.Generic;
using Utilities;

namespace PerfettoCds.Pipeline.DataOutput
{
    /// <summary>
    /// Contains information of processes seen during the trace (composite)
    /// </summary>
    public class PerfettoProcessEvent
    {
        public long Id { get; }
        public string Type { get; }

        /// <summary>
        /// Unique process id. This is != the OS pid.This is a monotonic number associated to each process
        /// The OS process id(pid) cannot be used as primary key because tids and pids are recycled by most kernels.
        /// </summary>
        public uint Upid { get; }

        /// <summary>
        /// The OS id for this process. Note: this is notunique over the lifetime of the trace so cannot be used as a primary key.
        /// </summary>
        public uint Pid { get; }

        /// <summary>
        /// The name of the process. Can be populated from manysources (e.g. ftrace, /proc scraping, track event etc).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The process name label populated from args or other means
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// The start timestamp of this process (if known). Isnull in most cases unless a process creation event is enabled (e.g. task_newtask ftrace event on Linux/Android).
        /// </summary>
        public Timestamp StartTimestamp { get; }

        /// <summary>
        /// The end timestamp of this process (if known). Isnull in most cases unless a process destruction event is enabled (e.g. sched_process_free ftrace event on Linux/Android).
        /// </summary>
        public Timestamp EndTimestamp { get; }

        /// <summary>
        /// The upid of the process which caused this process to be spawned
        /// </summary>
        public uint? ParentUpid { get; }

        /// <summary>
        /// The parent process which caused this process to be spawned
        /// </summary>
        public PerfettoProcessRawEvent ParentProcess { get; }

        /// <summary>
        /// The Unix user id of the process
        /// </summary>
        public uint? Uid { get; }

        /// <summary>
        /// Android appid of this process.
        /// </summary>
        public uint? AndroidAppId { get; }

        /// <summary>
        /// /proc/cmdline for this process.
        /// </summary>
        public string CmdLine { get; }

        // From Args table. The args for a process. Variable number per event
        /// <summary>
        /// Extra args for this process
        /// </summary>
        public Dictionary<string, object> Args { get; }
        public PerfettoPackageListEvent PackageList { get; }

        public PerfettoProcessEvent(long id, string type, uint upid, uint pid, string name, string label, Timestamp startTimestamp, Timestamp endTimestamp, uint? parentUpid, PerfettoProcessRawEvent parentProcess, uint? uid, uint? androidAppId, string cmdLine, Dictionary<string, object> args, PerfettoPackageListEvent packageList)
        {
            Id = id;
            Type = type;
            Upid = upid;
            Pid = pid;
            Name = Common.StringIntern(name);
            Label = Common.StringIntern(label);
            StartTimestamp = startTimestamp;
            EndTimestamp = endTimestamp;
            ParentUpid = parentUpid;
            ParentProcess = parentProcess;
            Uid = uid;
            AndroidAppId = androidAppId;
            CmdLine = cmdLine;
            Args = Args;
            PackageList = packageList;
        }
    }
}
