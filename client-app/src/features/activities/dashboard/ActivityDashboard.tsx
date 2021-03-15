import React, { useContext, useEffect } from 'react';
import { Grid } from 'semantic-ui-react';
import  ActivityList  from './ActivityList';
 

import {observer} from 'mobx-react-lite';

import { RootStoreContext } from '../../../app/stores/rootStore';
import { LoadingComponents } from '../../../app/layout/LoadingComponents';
 
const ActivityDashboard :React.FC= () => {
     
    const rootStore =  useContext(RootStoreContext)
    const {loadActivities , loadingInitial} = rootStore.activityStore;

   
    useEffect( () => {
     loadActivities();
   },[loadActivities])
  
   if   (loadingInitial ) return <LoadingComponents  content = 'Loading Activity ...' />
   
 
        return (
        <Grid>
            <Grid.Column width={10}>
           <ActivityList/>
            </Grid.Column>         
             <Grid.Column width={6}>
            </Grid.Column>
        
        </Grid>
         
    )
}


export default observer (ActivityDashboard);


//         {activity &&  !editMode  &&  
            //               <ActivityDetails/>
            //         }            
            //         { editMode &&   <ActivityForm key={activity && ( activity.id || 0 )} 
            //               activity = {activity!} 

            //              /> }

  