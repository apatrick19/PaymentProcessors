using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentProcessor.Core
{
    public class Entity
    {
        /// <summary>
        /// table ID
        /// </summary>
        [Key]
        public long ID { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public Entity()
        {
            this.DateCreated = DateTime.UtcNow;
        }
    }
}
