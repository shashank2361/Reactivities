using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Microsoft.AspNetCore.Mvc;
 using Domain;
using Persistance;
using MediatR;
using Application;
using Application.Activities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController
    {
 

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> List()
        {
             var listActivities = await Mediator.Send(new List.Query());
            return listActivities;
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ActivityDto>> Details(Guid id)
        {

            var activity =  await Mediator.Send(new Details.Query { Id = id});
            return activity;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]

        [Authorize(Policy ="IsActivityHost")]
        public async Task<ActionResult<Unit>> Edit (Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command); 
        }

        // POST: api/Activities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(  Create.Command command)
        {
            return await Mediator.Send(command); 
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsActivityHost")]

        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}/attend")]
        public async Task<ActionResult<Unit>> Attend(Guid id)
        {
            return await Mediator.Send(new Attend.Command { Id = id });            
        }

        [HttpDelete("{id}/attend")]
        public async Task<ActionResult<Unit>> Unattend(Guid id)
        {
            return await Mediator.Send(new Unattend.Command { Id = id });

        }
    }
}





//// GET: api/Activities
//[HttpGet]
//public async Task<ActionResult<List<ActivityDto>>> List()
//{
//    // return await _context.Activities.ToListAsync();
//    var listActivities = await Mediator.Send(new List.Query());
//    return listActivities;
//}

//// GET: api/Activities/5
//[HttpGet("{id}")]
//[Authorize]
//public async Task<ActionResult<ActivityDto>> Details(Guid id)
//{

//    var activity = await Mediator.Send(new Details.Query { Id = id });

//    //if (activity == null)
//    //{
//    //    return NotFound();
//    //}

//    return activity;
//}

//// PUT: api/Activities/5
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for
//// more details see https://aka.ms/RazorPagesCRUD.
//[HttpPut("{id}")]

//[Authorize(Policy = "IsActivityHost")]
//public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
//{

//    command.Id = id;
//    return await Mediator.Send(command);

//    //if (id != command.Id)
//    //{
//    //    return BadRequest();
//    //}
//    //_context.Entry(activity).State = EntityState.Modified;

//    //try
//    //{
//    //   return  await Mediator.SaveChangesAsync();
//    //}
//    //catch (DbUpdateConcurrencyException)
//    //{
//    //    if (!ActivityExists(id))
//    //    {
//    //        return NotFound();
//    //    }
//    //    else
//    //    {
//    //        throw;
//    //    }
//    //}

//    //return NoContent();
//}

//// POST: api/Activities
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for
//// more details see https://aka.ms/RazorPagesCRUD.
//[HttpPost]

//public async Task<ActionResult<Unit>> Create(Create.Command command)
//{
//    return await Mediator.Send(command);
//    //_context.Activities.Add(activity);
//    //await _context.SaveChangesAsync();
//    //return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
//}

//// DELETE: api/Activities/5
//[HttpDelete("{id}")]
//[Authorize(Policy = "IsActivityHost")]

//public async Task<ActionResult<Unit>> Delete(Guid id)
//{

//    return await Mediator.Send(new Delete.Command { Id = id });

//    //var activity = await _context.Activities.FindAsync(id);
//    //if (activity == null)
//    //{
//    //    return NotFound();
//    //}
//    //_context.Activities.Remove(activity);
//    //await _context.SaveChangesAsync();
//    //return activity;
//}

//[HttpPost("{id}/attend")]
//public async Task<ActionResult<Unit>> Attend(Guid id)
//{
//    return await Mediator.Send(new Attend.Command { Id = id });

//}



//[HttpDelete("{id}/attend")]
//public async Task<ActionResult<Unit>> Unattend(Guid id)
//{

//    return await Mediator.Send(new Unattend.Command { Id = id });


//}





