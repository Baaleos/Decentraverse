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
    
    
    //Function returns a string hash for the object for which this ERC721 token pertains
    function generateCelestialObject() public returns(string) {
        uint ten = Dice(theDiceAddress).dCustom(1,73); 
        return Hashes[ten];
    }
    
    string[] Hashes;
    
    function addHash(string hash) public {
        require(msg.sender == owner);
        Hashes.push(hash);
    }
}