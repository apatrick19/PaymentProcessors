using PaymentProcessor.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentProcessor.Data
{
    public class DbInitializer
    {
        
            public static void Initialize(PaymentProcessorContext context)
            {
                context.Database.EnsureCreated();

                // Look for any payment state.
                //if (context.PaymentState.Any())
                //{
                //    return;   // DB has been seeded
                //}

                //var students = new PaymentState[]
                //{
                //new PaymentState{Name=Enum.GetName(typeof(State), State.Pending)},
                //new PaymentState{Name=Enum.GetName(typeof(State), State.Processed)},
                //new PaymentState{Name=Enum.GetName(typeof(State), State.Failed)},
                              
                //};
                //context.PaymentState.AddRange(students);
                //context.SaveChanges();

            }
        }
    }

