pragma solidity ^0.4.24;

import './Owned.sol';
import './Dice.sol';
contract CelestialGenerator is Owned {
    constructor() public {
        
    }
    address theDiceAddress;
    function setDiceAddress(address addr) public {
        require(msg.sender == owner);
        theDiceAddress = addr;
    }
    
    
    function getHash(uint i) public view returns (string){
       
        
        if(i <= 10){
            //Earth
            return Hashes[0];
        }else if (i>= 11 && i <= 20){
            //Moon
            return Hashes[1];
            
        }else if(i>=21 && i<= 25){
            //Jupiter
            return Hashes[2];
        }else if(i>=26 && i<= 30){
            //Mars
            return Hashes[3];
        }else if(i>=31 && i<= 35){
            //mercury
            return Hashes[4];
        }else if(i>=36 && i<= 40){
            //nepture
            return Hashes[5];
        }else if(i>=41 && i<= 45){
            //saturn
            return Hashes[6];
        }else if(i>=46 && i<= 50){
            //uranus
            return Hashes[7];
        }else if(i>=51 && i<= 55){
            //venus
            return Hashes[8];
        }else if(i>=56 && i<= 58){
            //carina
            return Hashes[9];
        }else if(i>=59 && i<= 61){
            //hd209
            return Hashes[10];
        }else if(i>=62 && i<= 64){
            //m60
            return Hashes[11];
        }else if(i>=65 && i<= 67){
            //messier
            return Hashes[12];
        }else if(i>=68 && i<= 70){
            //sun
            return Hashes[13];
        }else if(i>=71 && i<= 73){
            //bubble
            return Hashes[14];
        }else if(i>=74 && i<= 76){
            //cone
            return Hashes[15];
        }else if(i>=77 && i<= 79){
            //veil
            return Hashes[16];
        }else if(i== 80){
            //blackhole
            return Hashes[17];
        }else if(i==81){
            //pismis
            return Hashes[18];
        }
    }
    
     
    //Function returns a string hash for the object for which this ERC721 token pertains
    function generateCelestialObject() public returns(string) {
        uint ten = Dice(theDiceAddress).dCustom(1,81);  
        return getHash(ten);
    }
    
    string[] Hashes;
    
    function addHash(string hash) public {
        require(msg.sender == owner);
        Hashes.push(hash);
    }
    function setHash(string hash, uint index) public {
        require(msg.sender == owner);
        Hashes[index] = hash;
    }
}