using System;

namespace Project2.Models
{
	public class GiftCard
	{

		public int id { get; set; }
		public int value { get; set; }
        public String store { get; set; }
        public DateTime expiryDate { get; set; }
        public Boolean expired { get; set; }

        public GiftCard(int id, int value, String store, DateTime expiryDate)
        {
            this.id = id;
            this.value = value;
            this.store = store;
            this.expiryDate = expiryDate;
            if (DateTime.Today.CompareTo(expiryDate) < 0)
            {
                this.expired = false;
            }
            else if (DateTime.Today.CompareTo(expiryDate) == 0)
            {
                this.expired = true;
            }
            else
            {
                this.expired = true;
            }
        }
    }

    public class CreateNewGiftCard
    {
        public int id { get; set; }
        public int value { get; set; }
        public String store { get; set; }
        public DateTime expiryDate { get; set; }

        public CreateNewGiftCard(int id, int value, string store, DateTime expiryDate)
        {
            this.id = id;
            this.value = value;
            this.store = store;
            this.expiryDate = expiryDate;
        }
    }

    public class AddGiftCardValue
    {
        public int id { get; set; }
        public int add { get; set; }

        public AddGiftCardValue(int id, int add)
        {
            this.id = id;
            this.add = add;
        }
    }

    public class SubstractGiftCardValue
    {
        public int id { get; set; }
        public int substract { get; set; }

        public SubstractGiftCardValue(int id, int substract)
        {
            this.id = id;
            this.substract = substract;
        }
    }

    public class ChangeGiftCardValue
    {
        public int id { get; set; }
        public int change { get; set; }

        public ChangeGiftCardValue(int id, int change)
        {
            this.id = id;
            this.change = change;
        }
    }

    public class RemoveGiftCardValue
    {
        public int id { get; set; }

        public RemoveGiftCardValue(int id)
        {
            this.id = id;
        }
    }
}

