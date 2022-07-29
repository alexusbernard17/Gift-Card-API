using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project2.Models;

namespace Project2ASP.Controllers
{
    [Route("GiftCard/")]
    [ApiController]
    public class GiftCardController : ControllerBase
    {
        public static List<GiftCard> giftCards = new List<GiftCard>()
        {
            new GiftCard(1300, 5, "Target", new DateTime(2022, 7, 1)),
            new GiftCard(1340, 100, "Publix", new DateTime(2022, 6, 1))
        };

        // Get All Gift Cards
        // GET: http://localhost:5138/GiftCard/giftcards?store=all
        [HttpGet("giftcards")]
        public String Get(String store)
        {
            if (giftCards.Count > 0)
            {
                if (store == "all")
                {
                    return JsonConvert.SerializeObject(giftCards);
                }
                else
                {
                    if (giftCards.Where(g => g.store == store).ToList().Count > 0)
                    {
                        return JsonConvert.SerializeObject(giftCards.Where(g => g.store == store).ToList());
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return JsonConvert.SerializeObject(new Error("There are no Gift Cards in this store", 404));
                    }
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("There are no Gift Cards in the database", 404));
        }

        // Get Gift Card by ID
        // GET: http://localhost:5138/GiftCard/1340
        [HttpGet("{id}", Name = "Get")]
        public String Get(int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (giftCards[i].id == id)
                {
                    return JsonConvert.SerializeObject(giftCards[i]);
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card Not Found", 404));
        }

        // Create New Gift Card
        // POST: http://localhost:5138/GiftCard
        [HttpPost]
        public String Post([FromBody] CreateNewGiftCard giftCard)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (giftCards[i].id == giftCard.id)
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Gift Card ID already exists", 400));
                }
                if (giftCard.value < 0)
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Gift Card value is negative", 400));
                }
            }
            giftCards.Add(new GiftCard(giftCard.id, giftCard.value, giftCard.store, giftCard.expiryDate));
            Response.StatusCode = 204;
            return null;
        }

        // Delete Gift Card from ID
        // DELETE: http://localhost:5138/GiftCard/1340
        [HttpDelete("{id}")]
        public String Delete(int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (giftCards[i].id == id)
                {
                    if (!giftCards[i].expired)
                    {
                        giftCards.RemoveAt(i);
                        Response.StatusCode = 204;
                        return null;
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return JsonConvert.SerializeObject(new Error("Gift Card is expired", 400));
                    }
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card ID does not exist", 404));
        }

        // Adding Gift Card Value
        // PATCH: http://localhost:5138/GiftCard/add/1300
        [HttpPatch("add/{id}")]
        public String Patch([FromBody] AddGiftCardValue giftCard, int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (giftCard.add < 0)
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Adding a negative value", 400));
                }
                if (id == giftCard.id)
                {
                    if (giftCards[i].id == giftCard.id)
                    {
                        if (!giftCards[i].expired)
                        {
                            giftCards[i].value += giftCard.add;
                            return JsonConvert.SerializeObject(giftCards[i]);
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return JsonConvert.SerializeObject(new Error("Gift Card is expired", 400));
                        }
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Gift Card ID does not match", 400));
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card ID does not exist", 404));
        }

        // Substract Gift Card Value
        // PATCH: http://localhost:5138/GiftCard/substract/1300
        [HttpPatch("substract/{id}")]
        public String Patch([FromBody] SubstractGiftCardValue giftCard, int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (id == giftCard.id)
                {
                    if (giftCards[i].id == giftCard.id)
                    {
                        if (giftCards[i].value - giftCard.substract < 0)
                        {
                            Response.StatusCode = 400;
                            return JsonConvert.SerializeObject(new Error("Gift Card value will be less than 0", 400));
                        }
                        giftCards[i].value -= giftCard.substract;
                        return JsonConvert.SerializeObject(giftCards[i]);
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Gift Card ID does not match", 400));
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card ID does not exist", 404));
        }

        // Changing Gift Card Value
        // PATCH: http://localhost:5138/GiftCard/change/1300
        [HttpPatch("change/{id}")]
        public String Patch([FromBody] ChangeGiftCardValue giftCard, int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (id == giftCard.id)
                {
                    if (giftCard.change < 0)
                    {
                        Response.StatusCode = 400;
                        return JsonConvert.SerializeObject(new Error("Changing to a negative value", 400));
                    }
                    if (giftCards[i].id == giftCard.id)
                    {
                        giftCards[i].value = giftCard.change;
                        return JsonConvert.SerializeObject(giftCards[i]);
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return JsonConvert.SerializeObject(new Error("Gift Card ID does not match", 400));
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card ID does not exist", 404));
        }

        // Removing Gift Card Value
        // PATCH: http://localhost:5138/GiftCard/remove/1300
        [HttpPatch("remove/{id}")]
        public String Patch(int id)
        {
            for (int i = 0; i < giftCards.Count; i++)
            {
                if (giftCards[i].id == id)
                {
                    giftCards[i].value = 0;
                    return JsonConvert.SerializeObject(giftCards[i]);
                }
            }
            Response.StatusCode = 404;
            return JsonConvert.SerializeObject(new Error("Gift Card ID does not exist", 404));
        }
    }
}
