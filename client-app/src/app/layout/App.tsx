import React ,{ Fragment, useContext, useEffect} from 'react';
import {  Container } from 'semantic-ui-react'
 import  NavBar  from '../../features/nav/NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { observer } from 'mobx-react-lite';

import {  Route , withRouter, RouteComponentProps, Switch} from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import ActivityForm from '../../features/activities/form/ActivityForm';
import ActivityDetails from '../../features/activities/details/ActivityDetails';
import NotFound from './NotFound';
import {ToastContainer} from 'react-toastify';
import LoginForm from '../../features/user/LoginForm';
import { RootStoreContext } from '../stores/rootStore';
import { LoadingComponents } from './LoadingComponents';
import ModalContainer from '../common/modals/ModalContainer';
import ProfilePage from '../../features/profiles/ProfilePage';



const App : React.FC<RouteComponentProps> = ({location}) =>   {
 
  const rootStore = useContext(RootStoreContext);
  const {setAppLoaded, token , appLoaded} = rootStore.commonStore;
  const {getUser} = rootStore.userStore;


  useEffect(() => {
    if(token){
      getUser().finally(() => setAppLoaded())
    }
    else{
      setAppLoaded();
    } 
  }, [getUser , setAppLoaded, token])


  if(!appLoaded){
    return <LoadingComponents content ='Loading app...' />
  }
  
  return (
     <Fragment>
      <ModalContainer/>
        <ToastContainer position='bottom-right' />
          <Route path= '/' exact component={HomePage}/> 
          <Route path= {'/(.+)'} render = {() => (
            <Fragment>
              <NavBar />
              <Container style ={{marginTop :'9em'}}>
              <Switch>
                  <Route path= '/activities'  exact component={ActivityDashboard}/> 
                  <Route path= '/activities/:id'  exact component={ActivityDetails}/> 
                  <Route key= {location.key} path= { ['/createActivity' , '/manage/:id']}  
                    component={ActivityForm}/> 
                  <Route path ='/profile/:username' component ={ProfilePage} />
                  // <Route path ='/login' component ={LoginForm} />

                  <Route  component={NotFound}/> 
              </Switch>
              </Container>  
          </Fragment>
        )}/> 
    </Fragment>
    );
  }

export default withRouter(observer(App));
// app is observing the observable