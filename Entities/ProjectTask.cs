﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Entities
{
    // Task is already in System.Threading.Tasks, so I named it ProjectTask
    public class ProjectTask : BaseEntity
    {

    }

    // Tried doing with enum, but thought it is hard to remember what 0, 1 or 2 mean when passing the data.
    // Also when looking up the data it is more understandable.
}
