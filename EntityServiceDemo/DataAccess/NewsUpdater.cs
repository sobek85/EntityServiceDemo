﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.EntityServiceDemo.Interfaces;
using MikeRobbins.EntityServiceDemo.Models;
using MikeRobbins.EntityServiceDemo.Utilties;
using Sitecore.Data.Items;

namespace MikeRobbins.EntityServiceDemo.DataAccess
{
    public class NewsUpdater : INewsUpdater
    {
        private IFieldUpdater _iFieldUpdater;
        private INewsReader _iNewsReader;
        private ISitecoreUtilities _iSitecoreUtilities;

        public NewsUpdater(IFieldUpdater iFieldUpdater, INewsReader iNewsReader, ISitecoreUtilities iSitecoreUtilities)
        {
            _iFieldUpdater = iFieldUpdater;
            _iNewsReader = iNewsReader;
            _iSitecoreUtilities = iSitecoreUtilities;
        }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            var id = _iSitecoreUtilities.ParseId(newsArticle.Id);

            if (!id.IsNull)
            {
                var item = _iNewsReader.GetNewsItem(id);

                _iFieldUpdater.AddFieldsToItem(item, newsArticle);
            }
        }

        public void DeleteNewsArticle(NewsArticle newsArticle)
        {
            var id = _iSitecoreUtilities.ParseId(newsArticle.Id);

            var item = _iNewsReader.GetNewsItem(id);

            item.Delete();
        }

    }
}