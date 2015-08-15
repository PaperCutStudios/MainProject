local gamePrefs = {}
gamePrefs.Difficulty = 1
gamePrefs.Time = 270
gamePrefs.NumPlayers = 0;
gamePrefs.Seed = 0
function gamePrefs.GetSeed()
    if (gamePrefs.Seed == nil or gamePrefs.Seed == 0) then
        gamePrefs.Seed = math.random(999999)
        print("Getting Seed: "..gamePrefs.Seed)
        return gamePrefs.Seed
    end
end
return gamePrefs
