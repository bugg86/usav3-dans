﻿using usav3dans.Services.Interfaces;
using Newtonsoft.Json;

namespace usav3dans.Services;

public class CaptainService : ICaptainService
{
    private readonly string captainFile = "../../../DB/captains.json";
    
    public Dictionary<string, int>? GetCaptains()
    {
        try
        {
            Dictionary<string, int>? captains =
                JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(captainFile));

            return captains;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public KeyValuePair<string, int>? GetSingleCaptain(string name)
    {
        Dictionary<string, int>? captains = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(captainFile));
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return null;
        }

        try
        {
            return new KeyValuePair<string, int>(name, captains[name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public bool AddCaptain(KeyValuePair<string, int> captain)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            captains.Add(captain.Key, captain.Value);
            
            if (captains.TryGetValue(captain.Key, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));

                Console.WriteLine(captain.Key + " successfully added");
                return true;
            }
            else
            {
                Console.WriteLine(captain.Key + " failed to be added");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool AddCaptains(Dictionary<string, int> captainsToAdd)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            foreach (KeyValuePair<string, int> captain in captainsToAdd)
            {
                captains.Add(captain.Key, captain.Value);
            }
            
            //Check if captains added here eventually
            
            File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
            
            return true; 
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool DeleteCaptainByName(string name)
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            captains.Remove(name);
            
            if (!captains.TryGetValue(name, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                
                Console.WriteLine(name + " successfully removed");
                return true;
            }
            else
            {
                Console.WriteLine(name + " failed to be removed");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool DeleteCaptainByObject(KeyValuePair<string, int> captain)    
    {
        Dictionary<string, int>? captains = GetCaptains();

        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            captains.Remove(captain.Key);
            
            if (!captains.TryGetValue(captain.Key, out int temp))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                
                Console.WriteLine(captain.Key + " successfully removed");
                return true;
            }
            else
            {
                Console.WriteLine(captain.Key + " failed to be removed");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool UpdateCaptain(KeyValuePair<string, int> newCaptain)
    {
        Dictionary<string, int>? captains = GetCaptains();
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            int oldBalance = captains[newCaptain.Key];
            captains[newCaptain.Key] = newCaptain.Value;
            
            if (!oldBalance.Equals(captains[newCaptain.Key]))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                
                Console.WriteLine(newCaptain.Key + " successfully updated");
                return true;
            }
            else
            {
                Console.WriteLine(newCaptain.Key + " failed to be updated");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool UpdateCaptain(string captain, int balance)
    {
        Dictionary<string, int>? captains = GetCaptains();
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            int oldBalance = captains[captain];
            captains[captain] = balance;
            
            if (!oldBalance.Equals(captains[captain]))
            {
                File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
                
                Console.WriteLine(captain + " successfully updated");
                return true;
            }
            else
            {
                Console.WriteLine(captain + " failed to be updated");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool ResetBalances()
    {
        Dictionary<string, int>? captains = GetCaptains();
        
        if (captains is null)
        {
            Console.WriteLine("captains.json does not exist");
            return false;
        }

        try
        {
            foreach (var captain in captains)
            {
                captains[captain.Key] = 1000;
            }
            
            File.WriteAllText(captainFile, JsonConvert.SerializeObject(captains, Formatting.Indented));
            
            Console.WriteLine("Captain balances reset to 1000.");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void GenerateDbFile()
    {
        if (!File.Exists(captainFile))
        {
            File.WriteAllText(captainFile, "{}");
        }
        else
        {
            Console.WriteLine("captains.json already exists!");
        }
    }
}