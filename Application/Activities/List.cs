using Application.Activities;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
 using System.Collections.Generic;
 using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public  class List
    {
        public class Query: IRequest<List<ActivityDto>> { }


        public class Handler : IRequestHandler<Query, List<ActivityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler( DataContext context, IMapper mapper )
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                //Eager Loading
                //var activities = await _context.Activities
                //                    .Include(x =>x.UserActivities)
                //                    .ThenInclude(x=>x.AppUser)
                //                    .ToListAsync();
                
                //Lazy loading
                var activities = await _context.Activities.ToListAsync();
                

                return  _mapper.Map<List<Activity> , List<ActivityDto>>(activities);
            }
        }
    }
}
