using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class RestaurantBusinessHourDtoService
    {
        /// <summary>
        /// Iterates through a list of RestaurantBusinessHourDtos, converts its DateTimes to the appropriate formats,
        /// and inserts them back into the Dto
        /// </summary>
        /// <param name="restaurantBusinessHourDtos"></param>
        /// <returns>IList<RestaurantBusinessHourDto></RestaurantProfileDto></returns>
        public IList<RestaurantBusinessHourDto> SetStringTimesFromDateTimes(IList<RestaurantBusinessHourDto> restaurantBusinessHourDtos)
        {
            foreach (var restaurantBusinessHourDto in restaurantBusinessHourDtos)
            {
                restaurantBusinessHourDto.OpenTime = restaurantBusinessHourDto.OpenDateTime.ToLocalTime().ToString("HH:mm");
                restaurantBusinessHourDto.CloseTime = restaurantBusinessHourDto.CloseDateTime.ToLocalTime().ToString("HH:mm");
                restaurantBusinessHourDto.TwelveHourFormatOpenTime = restaurantBusinessHourDto.OpenDateTime.ToLocalTime().ToString("hh:mm tt", CultureInfo.InvariantCulture);
                restaurantBusinessHourDto.TwelveHourFormatCloseTime = restaurantBusinessHourDto.CloseDateTime.ToLocalTime().ToString("hh:mm tt", CultureInfo.InvariantCulture);
            }
            return restaurantBusinessHourDtos;
        }

        /// <summary>
        /// Iterates through a list of RetaurantBusinessHourDtos, converts its OpenTime and CloseTime to DateTime type,
        /// and inserts them back into the Dto
        /// </summary>
        /// <param name="restaurantBusinessHourDtos"></param>
        /// <returns>IList<RestaurantBusinessHourDto></returns>
        public IList<RestaurantBusinessHourDto> SetDateTimesFromStringTimes(IList<RestaurantBusinessHourDto> restaurantBusinessHourDtos)
        {
            var dateTimeService = new DateTimeService();
            foreach (var restaurantBusinessHourDto in restaurantBusinessHourDtos)
            {
                restaurantBusinessHourDto.OpenDateTime = dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(restaurantBusinessHourDto.OpenTime), restaurantBusinessHourDto.TimeZone);
                restaurantBusinessHourDto.CloseDateTime = dateTimeService.ConvertLocalMeanTimeToUtc(dateTimeService.ConvertTimeToDateTimeUnspecifiedKind(restaurantBusinessHourDto.CloseTime), restaurantBusinessHourDto.TimeZone);
            }
            return restaurantBusinessHourDtos;
        }

        /// <summary>
        /// Iterates through a list of RestaurantBusinessHourDtos, and returns a list of BusinessHour domain models
        /// </summary>
        /// <param name="restaurantBusinessHourDtos"></param>
        /// <returns>IList<BusinessHour></returns>
        public IList<BusinessHour> MapRestaurantBusinessHourDtosToBusinessHourDomains (IList<RestaurantBusinessHourDto> restaurantBusinessHourDtos)
        {
            var businessHourDomains = restaurantBusinessHourDtos
                .Select(businessHourDto => new BusinessHour(
                    id: businessHourDto.Id,
                    day: businessHourDto.Day,
                    openTime: businessHourDto.OpenDateTime,
                    closeTime: businessHourDto.CloseDateTime,
                    flag: businessHourDto.Flag))
                .ToList();
            return businessHourDomains;
        }
    }
}
