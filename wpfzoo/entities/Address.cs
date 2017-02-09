using System;
using System.ComponentModel.DataAnnotations;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Address : BaseDBEntity
    {
        private String city;
        private StreetNumber streetNumber;
        private String street;
        private String postalCode;

        public Address()
        {

        }

        public Address(StreetNumber streetNumber)
        {
            this.StreetNumber = streetNumber;
        }

        [RegularExpression("[1-9]{1}[0-7]{1}[0-9]{3}", ErrorMessage ="PostalCode")]
        public String PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }

        public String Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }

        [Required(ErrorMessage="StreetNumber")]
        public StreetNumber StreetNumber
        {
            get { return streetNumber; }
            set
            {
                streetNumber = value;
                OnPropertyChanged("StreetNumber");
            }
            
        }


        public String City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                OnPropertyChanged("City");
            }
        }

    }
}