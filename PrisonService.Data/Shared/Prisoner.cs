﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    [BsonIgnoreExtraElements]
    public class Prisoner
    {
        private Prisoner(string fullname, string adress, DateTime birthday, string sex, string passport, 
            string familyStatus, string education, byte[] photo, string discription, string state, 
            DateTime dateOut, string prison, bool isArmmyReady, string sick, bool dietFood, string number, bool focus)
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            Fullname = fullname;
            Adress = adress;
            Birthday = birthday;
            Sex = sex;
            Passport = passport;
            FamilyStatus = familyStatus;
            Education = education;
            Photo = photo;
            Discription = discription;
            State = state;
            DateOut = dateOut;
            Prison = prison;
            IsArmmyReady = isArmmyReady;
            Sick = sick;
            DietFood = dietFood;
            Number = number;
            Focus = focus;
        }

        private Prisoner()
        { Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(); }

        [BsonId]
        public string Id { get; }
        public string Fullname { get; set; }
        public string Adress { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public string Passport { get; set; }
        public string FamilyStatus { get; set; }
        public string Education { get; set; }
        public byte[] Photo { get; set; }
        public string Discription { get; set; }
        public string State { get; set; }
        public DateTime DateOut { get; set; }
        public string Prison { get; set; }
        public bool IsArmmyReady { get; set; }
        public string Sick { get; set; }
        public bool DietFood { get; set; }
        public string Number { get; set; }
        public bool Focus { get; set; }

        public static Prisoner Create(string fullname, string adress, DateTime birthday, string sex, string passport,
            string familyStatus, string education, byte[] photo, string discription, string state,
            DateTime dateOut, string prison, bool isArmmyReady, string sick, bool dietFood, string number, bool focus)
        {
            return new Prisoner(fullname, adress, birthday, sex, passport,
            familyStatus, education, photo, discription, state,
            dateOut, prison, isArmmyReady, sick, dietFood, number, focus);
        }

        public static Prisoner CreateEmpty()
        {
            return new Prisoner();
        }
    }
}
