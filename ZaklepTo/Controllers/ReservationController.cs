﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllAsync();

            return Ok(reservations);
        }

        [HttpGet("{restaurantId}/{date}")]
        public async Task<IActionResult> GetAllConfirmedReservationsForSpecificRestaurantAndDate(Guid restaurantId, string date)
        {
            var reservations = await _reservationService.GetAllForSpecificRestaurantAndDate(restaurantId, date);

            return Ok(reservations);
        }

        [HttpGet("specific-restaurant/{restaurantId}")]
        public async Task<IActionResult> GetAllUnconffirmedReservationsForSpecificRestaurant(Guid restaurantId)
        {
            var reservations = await _reservationService.GetAllForSpecificRestaurant(restaurantId);

            return Ok(reservations);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveReservations()
        {
            var activeReservations = await _reservationService.GetAllActiveAsync();

            return Ok(activeReservations);
        }

        [HttpGet("unconfirmed")]
        public async Task<IActionResult> GetAllUnconfirmedReservations()
        {
            var unconfirmedReservations = await _reservationService.GetAllUncorfirmedReservationsAsync();

            return Ok(unconfirmedReservations);
        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetSingleReservation(Guid reservationId)
        {
            var reservation = await _reservationService.GetAsync(reservationId);

            return Ok(reservation);
        }

        [HttpGet("active/{customersLogin}")]
        public async Task<IActionResult> GetAllActiveReservationsByCustomer(string customersLogin)
        {
            var allActiveReservations = await _reservationService.GetAllActiveByCustomerAsync(customersLogin);

            return Ok(allActiveReservations);
        }

        [HttpGet("betweendates")]
        public async Task<IActionResult> GetAllReservationsBetweenDates([FromBody] TimeInterval timeInterval)
        {
            var reservationsBetweenDates = await _reservationService.GetAllBetweenDatesAsync(timeInterval);

            return Ok(reservationsBetweenDates);
        }

        [HttpPut("{reservationId}/update")]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationOnUpdateDto updatedReservation, Guid reservationId)
        {
            if (!ModelState.IsValid)
                return StatusCode(420, ModelState);

            await _reservationService.UpdateAsync(updatedReservation, reservationId);

            return Ok();
        }

        [HttpDelete("{reservationId}/remove")]
        public async Task<IActionResult> RemoveReservation(Guid reservationId)
        {
            await _reservationService.DeleteAsync(reservationId);

            return Ok();
        }

        [HttpPost("{reservationId}/deactivate")]
        public async Task<IActionResult> DeactivateReservation(Guid reservationId)
        {
            await _reservationService.DeactivateReservationAsync(reservationId);

            return Ok();
        }

        [HttpPost("{reservationId}/activate")]
        public async Task<IActionResult> ConfirmReservation(Guid reservationId)
        {
            await _reservationService.ConfirmReservationAsync(reservationId);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterReservation([FromBody] ReservationOnCreateDto reservation)
        {
            //TODO modelstate check
            await _reservationService.RegisterReservation(reservation);
            return Created("Reservation: ",Json(reservation));
        }
    }
}