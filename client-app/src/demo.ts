import React from 'react'

let data : number | string ;

data = '42';

export interface ICar {
    color : string ,
    model : string,
    topSpeed? : number

}
const car1 : ICar = {
    color : 'blue',
    model : 'BNW'
}
const car2 : ICar  = {
    color : 'red',
    model : 'Merc' ,
    topSpeed : 100
}

const multiply  = ( x :any , y : any)  : number => {
    return x * y;
}

export const cars=  [car1, car2];