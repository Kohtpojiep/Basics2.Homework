using System;
using System.Collections.Generic;
using System.Text;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;

namespace Basics2.Homework.BusinessLogic.Services
{
    public class ShowcaseProductService : IShowcaseProductService
    {
        private readonly IShowcaseProductRepository _showcaseProductRepository;

        public ShowcaseProductService(IShowcaseProductRepository showcaseProductRepository)
        {
            _showcaseProductRepository = showcaseProductRepository;
        }

        public ShowcaseProduct GetShowcaseProduct(int showcaseProductId)
        {
            return _showcaseProductRepository.Get(showcaseProductId);
        }

        public ShowcaseProduct[] GetShowcaseProducts(int[] showcaseProductIds)
        {
            List<ShowcaseProduct> showcaseProductsList = new List<ShowcaseProduct>();
            for (int i = 0; i < showcaseProductIds.Length; i++)
            {
               showcaseProductsList.Add(_showcaseProductRepository.Get(showcaseProductIds[i]));
            }

            return showcaseProductsList.ToArray();
        }

        public ShowcaseProduct CreateShowcaseProduct(ShowcaseProduct showcaseProduct)
        {
            return _showcaseProductRepository.Add(showcaseProduct);
        }

        public ShowcaseProduct[] CreateShowcaseProducts(ShowcaseProduct[] showcaseProducts)
        {
            return _showcaseProductRepository.Add(showcaseProducts);
        }

        public void UpdateShowcaseProduct(ShowcaseProduct showcaseProduct)
        {
            _showcaseProductRepository.Update(showcaseProduct);
        }

        public void UpdateShowcaseProducts(ShowcaseProduct[] showcaseProducts)
        {
            _showcaseProductRepository.Update(showcaseProducts);
        }

        public void RemoveShowcaseProduct(ShowcaseProduct showcaseProduct)
        {
            _showcaseProductRepository.Remove(showcaseProduct);
        }

        public void RemoveShowcaseProducts(ShowcaseProduct[] showcaseProducts)
        {
            _showcaseProductRepository.Remove(showcaseProducts);
        }
    }
}
