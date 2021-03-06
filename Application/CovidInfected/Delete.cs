using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistance;

namespace Application.Infected
{
    public class Delete 
    {
        public class Command : IRequest
        {
            public int Id{get;set;}
        }

        public class Handler :IRequestHandler<Command>
        {
            private readonly DataContext context;
            public Handler(DataContext context)
            {
                this.context=context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var infection = await this.context.CovidInfected.FindAsync(request.Id);
                if(infection==null)

                    throw new Exception("Could not find the person");

                    this.context.Remove(infection);
                    
                    var success = await this.context.SaveChangesAsync()>0;

                    if(success) return Unit.Value;
                    throw new Exception("Problem saving changes!");
                }
            }
        }
    }
