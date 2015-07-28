local player3 = {}
player3.ID = 3;
player3.IP = "";
player3.AnswerIDs = "";
function player3.SetID(id)
    player3.ID = id;
end
function player3.SetIP(ip)
    player3.IP = ip;
end
function player3.SetAnswer(answerid)
    player3.AnswerIDs = answerid;
end
return player3;
