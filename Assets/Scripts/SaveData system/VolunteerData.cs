using System;
[Serializable]
public class VolunteerData : IUserData
{
    private string volunteerName;
    public string volunteerPhone;
    public string Key { get => volunteerName; set => volunteerName = value; }
    public VolunteerData (string name , string phone)
    {
        volunteerName = name;
        volunteerPhone = phone;
    }

}


