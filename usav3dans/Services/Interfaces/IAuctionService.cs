﻿namespace usav3dans.Services.Interfaces;

public interface IAuctionService
{
    public string? GetStatus();
    public bool SetStatus(string status);
    public string GetCurrentPlayer();
    public bool SetCurrentPlayer(string player);
    public string GetHighestBid();
    public bool SetHighestBid(string bid);
    public string GetHighestBidder();
    public bool SetHighestBidder(string captain);
    public int GetSeconds();
    public bool SetSeconds(int seconds);
    public bool AddOneSecond();
    public void GenerateDbFile();
}