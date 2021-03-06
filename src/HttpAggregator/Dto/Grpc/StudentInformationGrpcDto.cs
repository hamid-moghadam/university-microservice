﻿using System;

namespace HttpAggregator.Dto.Grpc
{
    public class StudentInformationGrpcDto
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string FieldTitle { get; set; }
        public int FieldId { get; set; }
        public int CurrentSemesterId { get; set; }
        public string CurrentSemesterTitle { get; set; }

        public bool CanTakeCurriculums { get; set; }

        public DateTime? StartCurriculumScheduleDateTime { get; set; }

        public DateTime? EndCurriculumScheduleDateTime { get; set; }
    }
}