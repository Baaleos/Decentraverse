pragma solidity ^0.4.24;

contract Dice {
    constructor() public {
        
    }
    
    function d4() public returns (uint){
        return random(1,4);
    }
    function d6() public returns (uint){
        return random(1,6);
    }
    function d8() public returns (uint){
        return random(1,8);
    }
    function d10() public returns (uint){
        return random(1,10);
    }
    function d20() public returns (uint){
        return random(1,10);
    }
    function d50() public returns (uint){
        return random(1,50);
    }
    function d100() public returns (uint){
        return random(1,100);
    }
    
    function d1000() public returns (uint){
        return random(1,1000);
    }
    
    function d10000() public returns (uint){
        return random(1,10000);
    }
    uint nonce = 0;
    
    
    /* Generates a random number from 0 to 100 based on the last block hash */
    function random(uint min, uint max) public returns ( uint randomNumber) {
        nonce++;
        return(uint(keccak256 (abi.encodePacked(blockhash(block.number-min), nonce+block.timestamp )) )%max)+min;
    }

   
}
