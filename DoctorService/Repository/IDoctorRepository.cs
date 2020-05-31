using DoctorService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorService.Repository
{
    public interface IDoctorRepository
    {
        void CreateDiaryEntry(DoctorDiary objDoctorDiary);
        List<DoctorDiary> GetAllAppointments(int DoctorId);
    }
}
