using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public byte[] RowVersion { get; set; } // For optimistic concurrency control
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }


    }
}
