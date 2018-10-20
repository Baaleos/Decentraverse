using System;
using System.Collections.Generic;
using Decentraverse.Contracts;
using Decentraverse.Models;

namespace Decentraverse.Services
{
	public class InMemoryCardRepository : ICardRepository
    {
        private Card Jupiter = new Card("Jupiter", "https://img.purch.com/w/660/aHR0cDovL3d3dy5zcGFjZS5jb20vaW1hZ2VzL2kvMDAwLzAzOS8zMTgvb3JpZ2luYWwvanVwaXRlci1ncmVhdC1yZWQtc3BvdC5qcGc=",
                                        "Gas giant with red spot hundreds of kilometers in diameter.", Card.Rarity.UNIVERSAL);
        private Card Blackhole = new Card("NGC 4261 Blackhole", "https://upload.wikimedia.org/wikipedia/commons/4/4f/NGC_4261_Black_hole.jpg",
                                          "Blackhole at the centre of elliptical galaxy NGC-4261", Card.Rarity.SINGULAR);


        public Card GetCard(string hash)
        {
            return Jupiter;
        }

        public IEnumerable<Card> GetMyCards()
        {
            return new List<Card>
            {
                Jupiter,
                Blackhole
            };
        }
    }
}
