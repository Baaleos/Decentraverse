pragma solidity ^0.4.24;

import "./ERC721Full.sol";
import "./Dice.sol";
import "./CelestialGenerator.sol"; 
contract Decentraverse is ERC721 {
    constructor() public {
        Owner = msg.sender;
        TheDice = new Dice();
    }
    
    address theGeneratorAddress;
    Dice TheDice;
    
    
    function setGeneratorAddress(address addr) public {
        require(msg.sender == Owner);
        theGeneratorAddress = addr;
    }
    
    struct CelestialObject {
        string Hash; 
    }
    
    
    
    
    
    function createCelestialObject(address to) public {
       require(Owner == msg.sender);
       uint id = CelestialObjects.length;
       string memory hash = CelestialGenerator(theGeneratorAddress).generateCelestialObject();
       CelestialObjects.push(CelestialObject(hash));
       _mint(to,id);
    }  
    
    

    CelestialObject[] public CelestialObjects;
    address public Owner;
}