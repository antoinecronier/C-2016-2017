using System;

namespace wpfzoo.entities
{
    public class Address
    {
        private String city;
        private StreetNumber streetNumber;
        private String street;
        private String postalCode;

        public String PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public String Street
        {
            get { return street; }
            set { street = value; }
        }

        public StreetNumber StreetNumber
        {
            get { return streetNumber; }
            set { streetNumber = value; }
        }


        public String City
        {
            get { return this.city; }
            set { this.city = value; }
        }

    }
}