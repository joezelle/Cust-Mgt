﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Data.Entities
{
    public class Entity<TPrimaryKey>
    {
        public Entity()
        {
            IsActive = true;
            IsDeleted = false;
            DateCreated = DateTimeOffset.Now;
        }

        public TPrimaryKey Id { get; set; }
        public DateTimeOffset DateCreated { get; set; } 

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}