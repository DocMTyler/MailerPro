using MailerPro2.Data;
using MailerPro2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailerPro2.Services
{
    public class AddressService
    {
        private readonly ApplicationDbContext _address = new ApplicationDbContext();

        private readonly Guid _userID;

        public AddressService(Guid userID)
        {
            _userID = userID;
        }

        public bool AddAddress(AddressAdd model)
        {
            Address addressEntry = new Address
            {
                ID = model.ID,
                FullName = model.FullName,
                StreetAddress = model.StreetAddress,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode
            };

            _address.Addresses.Add(addressEntry);
            return _address.SaveChanges() == 1;
        }

        public IEnumerable<AddressListItem> ListAddress()
        {
            var addressEntries = _address.Addresses.ToList();

            var listOfAddresses = addressEntries.Select(a => new AddressListItem
            {
                ID = a.ID,
                FullName = a.FullName,
                StreetAddress = a.StreetAddress,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode
            }).ToList();

            return listOfAddresses;
        }

        public AddressListItem IndAddress(int id)
        {
            var addressEntry = _address.Addresses.Single(a => a.ID == id);

            return new AddressListItem
            {
                ID = addressEntry.ID,
                FullName = addressEntry.FullName,
                StreetAddress = addressEntry.StreetAddress,
                City = addressEntry.City,
                State = addressEntry.State,
                ZipCode = addressEntry.ZipCode
            };
        }

        public bool UpdateAddress(AddressUpdate model)
        {
            var addressEntry = _address.Addresses.Single(l => l.ID == model.ID);

            addressEntry.FullName = model.FullName;
            addressEntry.StreetAddress = model.StreetAddress;
            addressEntry.City = model.City;
            addressEntry.State = model.State;
            addressEntry.ZipCode = model.ZipCode;

            return _address.SaveChanges() == 1;
        }

        public bool DeleteAddress(int id)
        {
            var addressEntry = _address.Addresses.Single(l => l.ID == id);
            _address.Addresses.Remove(addressEntry);
            return _address.SaveChanges() == 1;
        }
    }
}
