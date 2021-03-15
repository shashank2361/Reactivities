import React from 'react';
import {ICar} from './demo';

interface IProps{
    car : ICar
}
const CarItem: React.FC<IProps> = ({car}) =>{
    return (
        <h1>{car.model}</h1>
    )
}
export default CarItem