import React, { useContext, useEffect } from 'react';
import {  Grid } from 'semantic-ui-react';
 import { observer } from 'mobx-react-lite';
import { RouteComponentProps } from 'react-router';
import { LoadingComponents } from '../../../app/layout/LoadingComponents';
 import ActivityDetailedHeader from './ActivityDetailedHeader';
import ActivityDetailedInfo from './ActivityDetailedInfo';
import ActivityDetailedChat from './ActivityDetailedChat';
import ActivityDetailedSidebar from './ActivityDetailedSidebar';
import { RootStoreContext } from '../../../app/stores/rootStore';

 
interface DetailParams{
  id:string;
}

const ActivityDetails : React.FC<RouteComponentProps<DetailParams>>  = ( {match , history } ) => {
 
  const rootStore =  useContext(RootStoreContext)


  const activityStore = rootStore.activityStore;


  
  const {activity   , loadActivity , loadingInitial} = activityStore;

  // loadActivity(match.params.id ).catch(() => {
  //   history.push('/NotFound')

  useEffect(() => {
     loadActivity(match.params.id );
     
  }, [loadActivity , match.params.id , history])

  
  console.log("act" ,activity )


if (loadingInitial) return <LoadingComponents content='Loading activity...' />;

if (!activity ) return <h2>Activity not found</h2>;
 

  return (
      <Grid>
          <Grid.Column width = {10} >
            <ActivityDetailedHeader activity ={activity}/>
            <ActivityDetailedInfo  activity ={activity}/>
            <ActivityDetailedChat/>
          </Grid.Column>

          <Grid.Column width = {6} >
            <ActivityDetailedSidebar attendees = {activity.attendees}/>



          </Grid.Column>
      </Grid>
  )
}


export default observer(ActivityDetails);











// <Card fluid>
// <Image src= {`/assets/categoryImages/${activity!.category}.jpg`} wrapped ui={false} />
// <Card.Content>
//   <Card.Header>{activity!.title}</Card.Header>
//   <Card.Meta>
//     <span className='date'>{activity!.date}</span>
//   </Card.Meta>
//   <Card.Description>
//     {activity!.description} 
//   </Card.Description>
// </Card.Content>
// <Card.Content extra>
// <Button.Group width={2}>
//     <Button
//     as = {Link} to = {`/manage/${activity.id}`}
//     // onClick ={ () => openEditForm(activity!.id)} 
//       basic color = "blue" 
//       content ="Edit" />
//     <Button  
//     onClick={() => history.push('/activities')}
//     basic color = "grey" 
//     content ="Cancel" />
// </Button.Group>  

// </Card.Content>
// </Card>



































