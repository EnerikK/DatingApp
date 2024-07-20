﻿using DatingApp.Domain.Validators.UserProfileValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates
{
    public class BasicInfo
    {
        private BasicInfo()
        {
            
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }

        //Factory Method
        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string emailAddress,
            string phone, DateTime dateOfBirth, string currentCity)
        {
            var validator = new BasicInfoValidator();

            var ObjToValidate = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                DateOfBirth = dateOfBirth,
                CurrentCity = currentCity
            };

            var validationResult = validator.Validate(ObjToValidate);
            if (validationResult.IsValid) return ObjToValidate;

            return ObjToValidate;
        }

    }
}