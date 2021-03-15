import {observable , action , computed ,  runInAction} from 'mobx';
import {  SyntheticEvent } from 'react';
import { IActivity } from '../models/activity';
import agent from '../api/agent';
import {history} from '../..';
import { toast } from 'react-toastify';
import { RootStore } from './rootStore';
import { setActivityProps, createAttendee } from '../common/util/util';
import { tr } from 'date-fns/locale';
import { triggerAsyncId } from 'async_hooks';


export default class ActivityStore  {

  rootStore :RootStore;
  constructor(rootStore :RootStore){
    this.rootStore = rootStore;
  }

    @observable activityRegistry = new Map();  
    @observable loadingInitial = false ;
    @observable  activity   : IActivity | null = null;

    @observable submitting = false;
    @observable target = '';

    @observable loading = false;
    @computed  get activitiesByDate () {

        return this.groupActivitiesByDate(Array.from(this.activityRegistry.values()));
    }


    groupActivitiesByDate (activities : IActivity[]){
      const sortedActivities = activities.sort(
        (a,b) => a.date.getTime() - b.date.getTime()
      )

      return Object.entries( sortedActivities.reduce((activities, activity) => {
        const date = activity.date.toISOString().split('T')[0];
         activities[date] = activities[date] ? [...activities[date] , activity ] : [activity];
        
        return activities;
        } ,{} as {[key : string] : IActivity[]}));
    }


    
    @action loadActivities = async () => {
      this.loadingInitial = true;
  
      try {
        const response = await   agent.Actvites.list();
        runInAction('loading activity' , () => {
          response.forEach( activity  => {
            setActivityProps(activity , this.rootStore.userStore.user!)
             this.activityRegistry.set(activity.id , activity)
        })        
    });
        this.loadingInitial = false;
        console.log(this.activityRegistry)
      }
      catch (error) { 
        runInAction('load activities error',  () => {
           this.loadingInitial = false;
        })
      }
  };


  @action loadActivity = async (id:string) => {
      let activity = this.getActivity(id);
      if(activity){
        this.activity = activity;
       return activity;
      }
      else{
        
        try {
          activity = await agent.Actvites.details(id);
          runInAction('getting activity' , () => {

            setActivityProps(activity , this.rootStore.userStore.user!)
              this.activity = activity;
              this.activityRegistry.set(activity.id ,activity)
              this.loadingInitial = false;
            })        
            return activity;
        }
        catch (error) { 
          runInAction('load  getting activity error',  () => {
             this.loadingInitial = false;
          })
          console.log("error",error);
        }
      }     
  }

  @action clearActivity = () => {
     this.activity = null;     
  };





getActivity = (id:string ) => {
  return this.activityRegistry.get(id)
}




  @action createActivity = async (activity: IActivity) => {
    this.submitting = true;
    try{
       await    agent.Actvites.create(activity);
       const attendee = createAttendee(this.rootStore.userStore.user!);
       attendee.isHost = true;
       let attendees = [];
       attendees.push(attendee);
       activity.attendees = attendees;
       activity.isHost = true;

      //this.activities.push(activity);
      runInAction( 'creating activity', () =>{
        this.activityRegistry.set(activity.id , activity)
         this.submitting = false;
      })
      history.push(`/activities/${activity.id}`)

    }
    catch(error){
    runInAction('create activity error', ()=> {
        this.submitting = false;
      })
      toast.error('Problem submitting data');
      console.log(error.response);
    }
  }


  @action editActivity = async (activity: IActivity) => {
    this.submitting = true;
    try{
      await agent.Actvites.update(activity);
        //this.activities.push(activity);
      runInAction( 'editing activity', () =>{
          this.activityRegistry.set(activity.id , activity)
          this.activity = activity;
          this.submitting = false;
      });
      history.push(`/activities/${activity.id}`)
    }
    catch(error){
      runInAction( 'edit activity error', () =>{
        this.submitting = false;      
      })
      toast.error('Problem submitting data');
      console.log(error);
    }
  }

  @action deleteActivity =  async (   event: SyntheticEvent<HTMLButtonElement> , id: string ) => {
  
    this.submitting = true;
    this.target = event.currentTarget.name;
    try{
    await agent.Actvites.delete(id)
    runInAction( 'delete activity', () =>{
      this.activityRegistry.delete(id);
      this.submitting = false;
      this.target = '';
    })
   }

   catch(error){
    runInAction( 'delete activity error', () =>{
      console.log(error)
      this.submitting = false;
      this.target = '';
    })
   }
  }


@action attendActivity = async () => {

  const attendee = createAttendee(this.rootStore.userStore.user!);
  this.loading = true;
  try {
    await agent.Actvites.attend(this.activity!.id);
    runInAction(() => {
      if(this.activity){
        this.activity.attendees.push(attendee);
        this.activity.isGoing = true;
        this.activityRegistry.set(this.activity.id , this.activity)
      }
      this.loading = false;

    } )
  }
  catch(error){
    runInAction(() => {
      this.loading = false;
    })
    toast.error('Problem signing up to activity')
  }

  }

  
@action cancelAttendance = async () => {
  this.loading = true;
  try{
    await agent.Actvites.unattend(this.activity!.id);
    runInAction(() => {
      if(this.activity){
        this.activity.attendees = this.activity.attendees.filter(a => a.username !== this.rootStore.userStore.user!.username);
        this.activity.isGoing = false;
        this.activityRegistry.set(this.activity.id , this.activity)
      }
     })
  }
  catch{
    runInAction(() => {
      this.loading = false;
    })
    toast.error('Problem cancelling attendance')
  }
  }
 
}




     // promise chain method
    // @action loadActivities = () => {
    //     this.loadingInitial = true;
    //     agent.Actvites.list().then( response => {
    //         response.forEach( activity  => {
    //           activity.date = activity.date.split('.')[0]
    //         this.activities.push(activity);
    //         })
    //        }).catch((error) => console.log(error))
    //        .finally(() => this.loadingInitial = false );
    // }


       // return this.activities.sort((a,b) => Date.parse(a.date) - Date.parse(b.date))
     //   return Array.from(this.activityRegistry.values()).sort((a,b) => Date.parse(a.date) - Date.parse(b.date));