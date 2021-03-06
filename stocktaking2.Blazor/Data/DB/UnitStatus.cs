﻿using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class UnitStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Unit> Units { get; set; }
        public DateTime DateCreated { get; set; }
        public UnitStatus()
        {
            Units = new List<Unit>();
            DateCreated = DateTime.Now;
        }
    }
}
