<a name='assembly'></a>
# Math.Tools.TrackReaders

## Contents

- [AbstractSource_t](#T-Math-Tools-TrackReaders-Tcx-AbstractSource_t 'Math.Tools.TrackReaders.Tcx.AbstractSource_t')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-AbstractSource_t-Name 'Math.Tools.TrackReaders.Tcx.AbstractSource_t.Name')
- [AbstractStep_t](#T-Math-Tools-TrackReaders-Tcx-AbstractStep_t 'Math.Tools.TrackReaders.Tcx.AbstractStep_t')
  - [StepId](#P-Math-Tools-TrackReaders-Tcx-AbstractStep_t-StepId 'Math.Tools.TrackReaders.Tcx.AbstractStep_t.StepId')
- [ActivityLap_t](#T-Math-Tools-TrackReaders-Tcx-ActivityLap_t 'Math.Tools.TrackReaders.Tcx.ActivityLap_t')
  - [AverageHeartRateBpm](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-AverageHeartRateBpm 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.AverageHeartRateBpm')
  - [Cadence](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Cadence 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Cadence')
  - [CadenceSpecified](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-CadenceSpecified 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.CadenceSpecified')
  - [Calories](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Calories 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Calories')
  - [DistanceMeters](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-DistanceMeters 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.DistanceMeters')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Extensions 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Extensions')
  - [Intensity](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Intensity 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Intensity')
  - [MaximumHeartRateBpm](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumHeartRateBpm 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.MaximumHeartRateBpm')
  - [MaximumSpeed](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumSpeed 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.MaximumSpeed')
  - [MaximumSpeedSpecified](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumSpeedSpecified 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.MaximumSpeedSpecified')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Notes 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Notes')
  - [StartTime](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-StartTime 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.StartTime')
  - [TotalTimeSeconds](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-TotalTimeSeconds 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.TotalTimeSeconds')
  - [Track](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Track 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.Track')
  - [TriggerMethod](#P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-TriggerMethod 'Math.Tools.TrackReaders.Tcx.ActivityLap_t.TriggerMethod')
- [ActivityList_t](#T-Math-Tools-TrackReaders-Tcx-ActivityList_t 'Math.Tools.TrackReaders.Tcx.ActivityList_t')
  - [Activity](#P-Math-Tools-TrackReaders-Tcx-ActivityList_t-Activity 'Math.Tools.TrackReaders.Tcx.ActivityList_t.Activity')
  - [MultiSportSession](#P-Math-Tools-TrackReaders-Tcx-ActivityList_t-MultiSportSession 'Math.Tools.TrackReaders.Tcx.ActivityList_t.MultiSportSession')
- [ActivityReference_t](#T-Math-Tools-TrackReaders-Tcx-ActivityReference_t 'Math.Tools.TrackReaders.Tcx.ActivityReference_t')
  - [Id](#P-Math-Tools-TrackReaders-Tcx-ActivityReference_t-Id 'Math.Tools.TrackReaders.Tcx.ActivityReference_t.Id')
- [Activity_t](#T-Math-Tools-TrackReaders-Tcx-Activity_t 'Math.Tools.TrackReaders.Tcx.Activity_t')
  - [Creator](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Creator 'Math.Tools.TrackReaders.Tcx.Activity_t.Creator')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Extensions 'Math.Tools.TrackReaders.Tcx.Activity_t.Extensions')
  - [Id](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Id 'Math.Tools.TrackReaders.Tcx.Activity_t.Id')
  - [Lap](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Lap 'Math.Tools.TrackReaders.Tcx.Activity_t.Lap')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Notes 'Math.Tools.TrackReaders.Tcx.Activity_t.Notes')
  - [Sport](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Sport 'Math.Tools.TrackReaders.Tcx.Activity_t.Sport')
  - [Training](#P-Math-Tools-TrackReaders-Tcx-Activity_t-Training 'Math.Tools.TrackReaders.Tcx.Activity_t.Training')
- [Application_t](#T-Math-Tools-TrackReaders-Tcx-Application_t 'Math.Tools.TrackReaders.Tcx.Application_t')
  - [Build](#P-Math-Tools-TrackReaders-Tcx-Application_t-Build 'Math.Tools.TrackReaders.Tcx.Application_t.Build')
  - [LangID](#P-Math-Tools-TrackReaders-Tcx-Application_t-LangID 'Math.Tools.TrackReaders.Tcx.Application_t.LangID')
  - [PartNumber](#P-Math-Tools-TrackReaders-Tcx-Application_t-PartNumber 'Math.Tools.TrackReaders.Tcx.Application_t.PartNumber')
- [BalloonStyle](#T-Math-Tools-TrackReaders-Kml-BalloonStyle 'Math.Tools.TrackReaders.Kml.BalloonStyle')
  - [text](#P-Math-Tools-TrackReaders-Kml-BalloonStyle-text 'Math.Tools.TrackReaders.Kml.BalloonStyle.text')
- [BuildType_t](#T-Math-Tools-TrackReaders-Tcx-BuildType_t 'Math.Tools.TrackReaders.Tcx.BuildType_t')
  - [Alpha](#F-Math-Tools-TrackReaders-Tcx-BuildType_t-Alpha 'Math.Tools.TrackReaders.Tcx.BuildType_t.Alpha')
  - [Beta](#F-Math-Tools-TrackReaders-Tcx-BuildType_t-Beta 'Math.Tools.TrackReaders.Tcx.BuildType_t.Beta')
  - [Internal](#F-Math-Tools-TrackReaders-Tcx-BuildType_t-Internal 'Math.Tools.TrackReaders.Tcx.BuildType_t.Internal')
  - [Release](#F-Math-Tools-TrackReaders-Tcx-BuildType_t-Release 'Math.Tools.TrackReaders.Tcx.BuildType_t.Release')
- [Build_t](#T-Math-Tools-TrackReaders-Tcx-Build_t 'Math.Tools.TrackReaders.Tcx.Build_t')
  - [Builder](#P-Math-Tools-TrackReaders-Tcx-Build_t-Builder 'Math.Tools.TrackReaders.Tcx.Build_t.Builder')
  - [Time](#P-Math-Tools-TrackReaders-Tcx-Build_t-Time 'Math.Tools.TrackReaders.Tcx.Build_t.Time')
  - [Type](#P-Math-Tools-TrackReaders-Tcx-Build_t-Type 'Math.Tools.TrackReaders.Tcx.Build_t.Type')
  - [TypeSpecified](#P-Math-Tools-TrackReaders-Tcx-Build_t-TypeSpecified 'Math.Tools.TrackReaders.Tcx.Build_t.TypeSpecified')
  - [Version](#P-Math-Tools-TrackReaders-Tcx-Build_t-Version 'Math.Tools.TrackReaders.Tcx.Build_t.Version')
- [Cadence_t](#T-Math-Tools-TrackReaders-Tcx-Cadence_t 'Math.Tools.TrackReaders.Tcx.Cadence_t')
  - [High](#P-Math-Tools-TrackReaders-Tcx-Cadence_t-High 'Math.Tools.TrackReaders.Tcx.Cadence_t.High')
  - [Low](#P-Math-Tools-TrackReaders-Tcx-Cadence_t-Low 'Math.Tools.TrackReaders.Tcx.Cadence_t.Low')
- [CaloriesBurned_t](#T-Math-Tools-TrackReaders-Tcx-CaloriesBurned_t 'Math.Tools.TrackReaders.Tcx.CaloriesBurned_t')
  - [Calories](#P-Math-Tools-TrackReaders-Tcx-CaloriesBurned_t-Calories 'Math.Tools.TrackReaders.Tcx.CaloriesBurned_t.Calories')
- [CourseFolder_t](#T-Math-Tools-TrackReaders-Tcx-CourseFolder_t 'Math.Tools.TrackReaders.Tcx.CourseFolder_t')
  - [CourseNameRef](#P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-CourseNameRef 'Math.Tools.TrackReaders.Tcx.CourseFolder_t.CourseNameRef')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Extensions 'Math.Tools.TrackReaders.Tcx.CourseFolder_t.Extensions')
  - [Folder](#P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Folder 'Math.Tools.TrackReaders.Tcx.CourseFolder_t.Folder')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Name 'Math.Tools.TrackReaders.Tcx.CourseFolder_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Notes 'Math.Tools.TrackReaders.Tcx.CourseFolder_t.Notes')
- [CourseLap_t](#T-Math-Tools-TrackReaders-Tcx-CourseLap_t 'Math.Tools.TrackReaders.Tcx.CourseLap_t')
  - [AverageHeartRateBpm](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-AverageHeartRateBpm 'Math.Tools.TrackReaders.Tcx.CourseLap_t.AverageHeartRateBpm')
  - [BeginAltitudeMeters](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginAltitudeMeters 'Math.Tools.TrackReaders.Tcx.CourseLap_t.BeginAltitudeMeters')
  - [BeginAltitudeMetersSpecified](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginAltitudeMetersSpecified 'Math.Tools.TrackReaders.Tcx.CourseLap_t.BeginAltitudeMetersSpecified')
  - [BeginPosition](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginPosition 'Math.Tools.TrackReaders.Tcx.CourseLap_t.BeginPosition')
  - [Cadence](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Cadence 'Math.Tools.TrackReaders.Tcx.CourseLap_t.Cadence')
  - [CadenceSpecified](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-CadenceSpecified 'Math.Tools.TrackReaders.Tcx.CourseLap_t.CadenceSpecified')
  - [DistanceMeters](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-DistanceMeters 'Math.Tools.TrackReaders.Tcx.CourseLap_t.DistanceMeters')
  - [EndAltitudeMeters](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndAltitudeMeters 'Math.Tools.TrackReaders.Tcx.CourseLap_t.EndAltitudeMeters')
  - [EndAltitudeMetersSpecified](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndAltitudeMetersSpecified 'Math.Tools.TrackReaders.Tcx.CourseLap_t.EndAltitudeMetersSpecified')
  - [EndPosition](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndPosition 'Math.Tools.TrackReaders.Tcx.CourseLap_t.EndPosition')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Extensions 'Math.Tools.TrackReaders.Tcx.CourseLap_t.Extensions')
  - [Intensity](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Intensity 'Math.Tools.TrackReaders.Tcx.CourseLap_t.Intensity')
  - [MaximumHeartRateBpm](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-MaximumHeartRateBpm 'Math.Tools.TrackReaders.Tcx.CourseLap_t.MaximumHeartRateBpm')
  - [TotalTimeSeconds](#P-Math-Tools-TrackReaders-Tcx-CourseLap_t-TotalTimeSeconds 'Math.Tools.TrackReaders.Tcx.CourseLap_t.TotalTimeSeconds')
- [CoursePointType_t](#T-Math-Tools-TrackReaders-Tcx-CoursePointType_t 'Math.Tools.TrackReaders.Tcx.CoursePointType_t')
  - [Danger](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Danger 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Danger')
  - [FirstAid](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-FirstAid 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.FirstAid')
  - [Food](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Food 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Food')
  - [Generic](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Generic 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Generic')
  - [HorsCategory](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-HorsCategory 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.HorsCategory')
  - [Item1stCategory](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item1stCategory 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Item1stCategory')
  - [Item2ndCategory](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item2ndCategory 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Item2ndCategory')
  - [Item3rdCategory](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item3rdCategory 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Item3rdCategory')
  - [Item4thCategory](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item4thCategory 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Item4thCategory')
  - [Left](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Left 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Left')
  - [Right](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Right 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Right')
  - [Sprint](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Sprint 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Sprint')
  - [Straight](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Straight 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Straight')
  - [Summit](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Summit 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Summit')
  - [Valley](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Valley 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Valley')
  - [Water](#F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Water 'Math.Tools.TrackReaders.Tcx.CoursePointType_t.Water')
- [CoursePoint_t](#T-Math-Tools-TrackReaders-Tcx-CoursePoint_t 'Math.Tools.TrackReaders.Tcx.CoursePoint_t')
  - [AltitudeMeters](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-AltitudeMeters 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.AltitudeMeters')
  - [AltitudeMetersSpecified](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-AltitudeMetersSpecified 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.AltitudeMetersSpecified')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Extensions 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.Extensions')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Name 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Notes 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.Notes')
  - [PointType](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-PointType 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.PointType')
  - [Position](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Position 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.Position')
  - [Time](#P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Time 'Math.Tools.TrackReaders.Tcx.CoursePoint_t.Time')
- [Course_t](#T-Math-Tools-TrackReaders-Tcx-Course_t 'Math.Tools.TrackReaders.Tcx.Course_t')
  - [CoursePoint](#P-Math-Tools-TrackReaders-Tcx-Course_t-CoursePoint 'Math.Tools.TrackReaders.Tcx.Course_t.CoursePoint')
  - [Creator](#P-Math-Tools-TrackReaders-Tcx-Course_t-Creator 'Math.Tools.TrackReaders.Tcx.Course_t.Creator')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Course_t-Extensions 'Math.Tools.TrackReaders.Tcx.Course_t.Extensions')
  - [Lap](#P-Math-Tools-TrackReaders-Tcx-Course_t-Lap 'Math.Tools.TrackReaders.Tcx.Course_t.Lap')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-Course_t-Name 'Math.Tools.TrackReaders.Tcx.Course_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-Course_t-Notes 'Math.Tools.TrackReaders.Tcx.Course_t.Notes')
  - [Track](#P-Math-Tools-TrackReaders-Tcx-Course_t-Track 'Math.Tools.TrackReaders.Tcx.Course_t.Track')
- [Courses_t](#T-Math-Tools-TrackReaders-Tcx-Courses_t 'Math.Tools.TrackReaders.Tcx.Courses_t')
  - [CourseFolder](#P-Math-Tools-TrackReaders-Tcx-Courses_t-CourseFolder 'Math.Tools.TrackReaders.Tcx.Courses_t.CourseFolder')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Courses_t-Extensions 'Math.Tools.TrackReaders.Tcx.Courses_t.Extensions')
- [CustomHeartRateZone_t](#T-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t 'Math.Tools.TrackReaders.Tcx.CustomHeartRateZone_t')
  - [High](#P-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t-High 'Math.Tools.TrackReaders.Tcx.CustomHeartRateZone_t.High')
  - [Low](#P-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t-Low 'Math.Tools.TrackReaders.Tcx.CustomHeartRateZone_t.Low')
- [CustomSpeedZone_t](#T-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t 'Math.Tools.TrackReaders.Tcx.CustomSpeedZone_t')
  - [HighInMetersPerSecond](#P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-HighInMetersPerSecond 'Math.Tools.TrackReaders.Tcx.CustomSpeedZone_t.HighInMetersPerSecond')
  - [LowInMetersPerSecond](#P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-LowInMetersPerSecond 'Math.Tools.TrackReaders.Tcx.CustomSpeedZone_t.LowInMetersPerSecond')
  - [ViewAs](#P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-ViewAs 'Math.Tools.TrackReaders.Tcx.CustomSpeedZone_t.ViewAs')
- [Deserializer](#T-Math-Tools-TrackReaders-Deserializer 'Math.Tools.TrackReaders.Deserializer')
  - [Directory(path)](#M-Math-Tools-TrackReaders-Deserializer-Directory-System-String- 'Math.Tools.TrackReaders.Deserializer.Directory(System.String)')
  - [File(filename)](#M-Math-Tools-TrackReaders-Deserializer-File-System-String- 'Math.Tools.TrackReaders.Deserializer.File(System.String)')
  - [String(input)](#M-Math-Tools-TrackReaders-Deserializer-String-System-String- 'Math.Tools.TrackReaders.Deserializer.String(System.String)')
- [Device_t](#T-Math-Tools-TrackReaders-Tcx-Device_t 'Math.Tools.TrackReaders.Tcx.Device_t')
  - [ProductID](#P-Math-Tools-TrackReaders-Tcx-Device_t-ProductID 'Math.Tools.TrackReaders.Tcx.Device_t.ProductID')
  - [UnitId](#P-Math-Tools-TrackReaders-Tcx-Device_t-UnitId 'Math.Tools.TrackReaders.Tcx.Device_t.UnitId')
  - [Version](#P-Math-Tools-TrackReaders-Tcx-Device_t-Version 'Math.Tools.TrackReaders.Tcx.Device_t.Version')
- [Distance_t](#T-Math-Tools-TrackReaders-Tcx-Distance_t 'Math.Tools.TrackReaders.Tcx.Distance_t')
  - [Meters](#P-Math-Tools-TrackReaders-Tcx-Distance_t-Meters 'Math.Tools.TrackReaders.Tcx.Distance_t.Meters')
- [Document](#T-Math-Tools-TrackReaders-Kml-Document 'Math.Tools.TrackReaders.Kml.Document')
  - [Folder](#P-Math-Tools-TrackReaders-Kml-Document-Folder 'Math.Tools.TrackReaders.Kml.Document.Folder')
  - [LookAt](#P-Math-Tools-TrackReaders-Kml-Document-LookAt 'Math.Tools.TrackReaders.Kml.Document.LookAt')
  - [Placemark](#P-Math-Tools-TrackReaders-Kml-Document-Placemark 'Math.Tools.TrackReaders.Kml.Document.Placemark')
  - [Style](#P-Math-Tools-TrackReaders-Kml-Document-Style 'Math.Tools.TrackReaders.Kml.Document.Style')
  - [StyleMap](#P-Math-Tools-TrackReaders-Kml-Document-StyleMap 'Math.Tools.TrackReaders.Kml.Document.StyleMap')
  - [description](#P-Math-Tools-TrackReaders-Kml-Document-description 'Math.Tools.TrackReaders.Kml.Document.description')
  - [name](#P-Math-Tools-TrackReaders-Kml-Document-name 'Math.Tools.TrackReaders.Kml.Document.name')
  - [open](#P-Math-Tools-TrackReaders-Kml-Document-open 'Math.Tools.TrackReaders.Kml.Document.open')
  - [openSpecified](#P-Math-Tools-TrackReaders-Kml-Document-openSpecified 'Math.Tools.TrackReaders.Kml.Document.openSpecified')
  - [visibility](#P-Math-Tools-TrackReaders-Kml-Document-visibility 'Math.Tools.TrackReaders.Kml.Document.visibility')
  - [visibilitySpecified](#P-Math-Tools-TrackReaders-Kml-Document-visibilitySpecified 'Math.Tools.TrackReaders.Kml.Document.visibilitySpecified')
- [Duration_t](#T-Math-Tools-TrackReaders-Tcx-Duration_t 'Math.Tools.TrackReaders.Tcx.Duration_t')
- [Extensions_t](#T-Math-Tools-TrackReaders-Gpx-Extensions_t 'Math.Tools.TrackReaders.Gpx.Extensions_t')
- [Extensions_t](#T-Math-Tools-TrackReaders-Tcx-Extensions_t 'Math.Tools.TrackReaders.Tcx.Extensions_t')
  - [Any](#P-Math-Tools-TrackReaders-Gpx-Extensions_t-Any 'Math.Tools.TrackReaders.Gpx.Extensions_t.Any')
  - [Any](#P-Math-Tools-TrackReaders-Tcx-Extensions_t-Any 'Math.Tools.TrackReaders.Tcx.Extensions_t.Any')
- [FirstSport_t](#T-Math-Tools-TrackReaders-Tcx-FirstSport_t 'Math.Tools.TrackReaders.Tcx.FirstSport_t')
  - [Activity](#P-Math-Tools-TrackReaders-Tcx-FirstSport_t-Activity 'Math.Tools.TrackReaders.Tcx.FirstSport_t.Activity')
- [Folder](#T-Math-Tools-TrackReaders-Kml-Folder 'Math.Tools.TrackReaders.Kml.Folder')
  - [Document](#P-Math-Tools-TrackReaders-Kml-Folder-Document 'Math.Tools.TrackReaders.Kml.Folder.Document')
  - [Folder1](#P-Math-Tools-TrackReaders-Kml-Folder-Folder1 'Math.Tools.TrackReaders.Kml.Folder.Folder1')
  - [GroundOverlay](#P-Math-Tools-TrackReaders-Kml-Folder-GroundOverlay 'Math.Tools.TrackReaders.Kml.Folder.GroundOverlay')
  - [LookAt](#P-Math-Tools-TrackReaders-Kml-Folder-LookAt 'Math.Tools.TrackReaders.Kml.Folder.LookAt')
  - [Placemark](#P-Math-Tools-TrackReaders-Kml-Folder-Placemark 'Math.Tools.TrackReaders.Kml.Folder.Placemark')
  - [ScreenOverlay](#P-Math-Tools-TrackReaders-Kml-Folder-ScreenOverlay 'Math.Tools.TrackReaders.Kml.Folder.ScreenOverlay')
  - [description](#P-Math-Tools-TrackReaders-Kml-Folder-description 'Math.Tools.TrackReaders.Kml.Folder.description')
  - [name](#P-Math-Tools-TrackReaders-Kml-Folder-name 'Math.Tools.TrackReaders.Kml.Folder.name')
  - [styleUrl](#P-Math-Tools-TrackReaders-Kml-Folder-styleUrl 'Math.Tools.TrackReaders.Kml.Folder.styleUrl')
  - [visibility](#P-Math-Tools-TrackReaders-Kml-Folder-visibility 'Math.Tools.TrackReaders.Kml.Folder.visibility')
  - [visibilitySpecified](#P-Math-Tools-TrackReaders-Kml-Folder-visibilitySpecified 'Math.Tools.TrackReaders.Kml.Folder.visibilitySpecified')
- [Folders_t](#T-Math-Tools-TrackReaders-Tcx-Folders_t 'Math.Tools.TrackReaders.Tcx.Folders_t')
  - [Courses](#P-Math-Tools-TrackReaders-Tcx-Folders_t-Courses 'Math.Tools.TrackReaders.Tcx.Folders_t.Courses')
  - [History](#P-Math-Tools-TrackReaders-Tcx-Folders_t-History 'Math.Tools.TrackReaders.Tcx.Folders_t.History')
  - [Workouts](#P-Math-Tools-TrackReaders-Tcx-Folders_t-Workouts 'Math.Tools.TrackReaders.Tcx.Folders_t.Workouts')
- [GpxConverter](#T-Math-Tools-TrackReaders-GpxConverter 'Math.Tools.TrackReaders.GpxConverter')
  - [Convert(data)](#M-Math-Tools-TrackReaders-GpxConverter-Convert-Math-Tools-TrackReaders-Gpx-gpx- 'Math.Tools.TrackReaders.GpxConverter.Convert(Math.Tools.TrackReaders.Gpx.gpx)')
- [GroundOverlay](#T-Math-Tools-TrackReaders-Kml-GroundOverlay 'Math.Tools.TrackReaders.Kml.GroundOverlay')
  - [Icon](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-Icon 'Math.Tools.TrackReaders.Kml.GroundOverlay.Icon')
  - [LatLonBox](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-LatLonBox 'Math.Tools.TrackReaders.Kml.GroundOverlay.LatLonBox')
  - [LookAt](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-LookAt 'Math.Tools.TrackReaders.Kml.GroundOverlay.LookAt')
  - [description](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-description 'Math.Tools.TrackReaders.Kml.GroundOverlay.description')
  - [name](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-name 'Math.Tools.TrackReaders.Kml.GroundOverlay.name')
  - [visibility](#P-Math-Tools-TrackReaders-Kml-GroundOverlay-visibility 'Math.Tools.TrackReaders.Kml.GroundOverlay.visibility')
- [HeartRateAbove_t](#T-Math-Tools-TrackReaders-Tcx-HeartRateAbove_t 'Math.Tools.TrackReaders.Tcx.HeartRateAbove_t')
  - [HeartRate](#P-Math-Tools-TrackReaders-Tcx-HeartRateAbove_t-HeartRate 'Math.Tools.TrackReaders.Tcx.HeartRateAbove_t.HeartRate')
- [HeartRateAsPercentOfMax_t](#T-Math-Tools-TrackReaders-Tcx-HeartRateAsPercentOfMax_t 'Math.Tools.TrackReaders.Tcx.HeartRateAsPercentOfMax_t')
  - [Value](#P-Math-Tools-TrackReaders-Tcx-HeartRateAsPercentOfMax_t-Value 'Math.Tools.TrackReaders.Tcx.HeartRateAsPercentOfMax_t.Value')
- [HeartRateBelow_t](#T-Math-Tools-TrackReaders-Tcx-HeartRateBelow_t 'Math.Tools.TrackReaders.Tcx.HeartRateBelow_t')
  - [HeartRate](#P-Math-Tools-TrackReaders-Tcx-HeartRateBelow_t-HeartRate 'Math.Tools.TrackReaders.Tcx.HeartRateBelow_t.HeartRate')
- [HeartRateInBeatsPerMinute_t](#T-Math-Tools-TrackReaders-Tcx-HeartRateInBeatsPerMinute_t 'Math.Tools.TrackReaders.Tcx.HeartRateInBeatsPerMinute_t')
  - [Value](#P-Math-Tools-TrackReaders-Tcx-HeartRateInBeatsPerMinute_t-Value 'Math.Tools.TrackReaders.Tcx.HeartRateInBeatsPerMinute_t.Value')
- [HeartRateValue_t](#T-Math-Tools-TrackReaders-Tcx-HeartRateValue_t 'Math.Tools.TrackReaders.Tcx.HeartRateValue_t')
- [HeartRate_t](#T-Math-Tools-TrackReaders-Tcx-HeartRate_t 'Math.Tools.TrackReaders.Tcx.HeartRate_t')
  - [HeartRateZone](#P-Math-Tools-TrackReaders-Tcx-HeartRate_t-HeartRateZone 'Math.Tools.TrackReaders.Tcx.HeartRate_t.HeartRateZone')
- [HistoryFolder_t](#T-Math-Tools-TrackReaders-Tcx-HistoryFolder_t 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t')
  - [ActivityRef](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-ActivityRef 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.ActivityRef')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Extensions 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.Extensions')
  - [Folder](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Folder 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.Folder')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Name 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Notes 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.Notes')
  - [Week](#P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Week 'Math.Tools.TrackReaders.Tcx.HistoryFolder_t.Week')
- [History_t](#T-Math-Tools-TrackReaders-Tcx-History_t 'Math.Tools.TrackReaders.Tcx.History_t')
  - [Biking](#P-Math-Tools-TrackReaders-Tcx-History_t-Biking 'Math.Tools.TrackReaders.Tcx.History_t.Biking')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-History_t-Extensions 'Math.Tools.TrackReaders.Tcx.History_t.Extensions')
  - [MultiSport](#P-Math-Tools-TrackReaders-Tcx-History_t-MultiSport 'Math.Tools.TrackReaders.Tcx.History_t.MultiSport')
  - [Other](#P-Math-Tools-TrackReaders-Tcx-History_t-Other 'Math.Tools.TrackReaders.Tcx.History_t.Other')
  - [Running](#P-Math-Tools-TrackReaders-Tcx-History_t-Running 'Math.Tools.TrackReaders.Tcx.History_t.Running')
- [Icon](#T-Math-Tools-TrackReaders-Kml-Icon 'Math.Tools.TrackReaders.Kml.Icon')
  - [href](#P-Math-Tools-TrackReaders-Kml-Icon-href 'Math.Tools.TrackReaders.Kml.Icon.href')
- [IconStyle](#T-Math-Tools-TrackReaders-Kml-IconStyle 'Math.Tools.TrackReaders.Kml.IconStyle')
  - [Icon](#P-Math-Tools-TrackReaders-Kml-IconStyle-Icon 'Math.Tools.TrackReaders.Kml.IconStyle.Icon')
- [Intensity_t](#T-Math-Tools-TrackReaders-Tcx-Intensity_t 'Math.Tools.TrackReaders.Tcx.Intensity_t')
  - [Active](#F-Math-Tools-TrackReaders-Tcx-Intensity_t-Active 'Math.Tools.TrackReaders.Tcx.Intensity_t.Active')
  - [Resting](#F-Math-Tools-TrackReaders-Tcx-Intensity_t-Resting 'Math.Tools.TrackReaders.Tcx.Intensity_t.Resting')
- [KmlConverter](#T-Math-Tools-TrackReaders-KmlConverter 'Math.Tools.TrackReaders.KmlConverter')
  - [Convert(data)](#M-Math-Tools-TrackReaders-KmlConverter-Convert-Math-Tools-TrackReaders-Kml-kml- 'Math.Tools.TrackReaders.KmlConverter.Convert(Math.Tools.TrackReaders.Kml.kml)')
- [LatLonBox](#T-Math-Tools-TrackReaders-Kml-LatLonBox 'Math.Tools.TrackReaders.Kml.LatLonBox')
  - [east](#P-Math-Tools-TrackReaders-Kml-LatLonBox-east 'Math.Tools.TrackReaders.Kml.LatLonBox.east')
  - [north](#P-Math-Tools-TrackReaders-Kml-LatLonBox-north 'Math.Tools.TrackReaders.Kml.LatLonBox.north')
  - [rotation](#P-Math-Tools-TrackReaders-Kml-LatLonBox-rotation 'Math.Tools.TrackReaders.Kml.LatLonBox.rotation')
  - [south](#P-Math-Tools-TrackReaders-Kml-LatLonBox-south 'Math.Tools.TrackReaders.Kml.LatLonBox.south')
  - [west](#P-Math-Tools-TrackReaders-Kml-LatLonBox-west 'Math.Tools.TrackReaders.Kml.LatLonBox.west')
- [LineString](#T-Math-Tools-TrackReaders-Kml-LineString 'Math.Tools.TrackReaders.Kml.LineString')
  - [altitudeMode](#P-Math-Tools-TrackReaders-Kml-LineString-altitudeMode 'Math.Tools.TrackReaders.Kml.LineString.altitudeMode')
  - [altitudeModeSpecified](#P-Math-Tools-TrackReaders-Kml-LineString-altitudeModeSpecified 'Math.Tools.TrackReaders.Kml.LineString.altitudeModeSpecified')
  - [coordinates](#P-Math-Tools-TrackReaders-Kml-LineString-coordinates 'Math.Tools.TrackReaders.Kml.LineString.coordinates')
  - [extrude](#P-Math-Tools-TrackReaders-Kml-LineString-extrude 'Math.Tools.TrackReaders.Kml.LineString.extrude')
  - [extrudeSpecified](#P-Math-Tools-TrackReaders-Kml-LineString-extrudeSpecified 'Math.Tools.TrackReaders.Kml.LineString.extrudeSpecified')
  - [tessellate](#P-Math-Tools-TrackReaders-Kml-LineString-tessellate 'Math.Tools.TrackReaders.Kml.LineString.tessellate')
- [LineStyle](#T-Math-Tools-TrackReaders-Kml-LineStyle 'Math.Tools.TrackReaders.Kml.LineStyle')
  - [color](#P-Math-Tools-TrackReaders-Kml-LineStyle-color 'Math.Tools.TrackReaders.Kml.LineStyle.color')
  - [width](#P-Math-Tools-TrackReaders-Kml-LineStyle-width 'Math.Tools.TrackReaders.Kml.LineStyle.width')
  - [widthSpecified](#P-Math-Tools-TrackReaders-Kml-LineStyle-widthSpecified 'Math.Tools.TrackReaders.Kml.LineStyle.widthSpecified')
- [LinearRing](#T-Math-Tools-TrackReaders-Kml-LinearRing 'Math.Tools.TrackReaders.Kml.LinearRing')
  - [coordinates](#P-Math-Tools-TrackReaders-Kml-LinearRing-coordinates 'Math.Tools.TrackReaders.Kml.LinearRing.coordinates')
- [LookAt](#T-Math-Tools-TrackReaders-Kml-LookAt 'Math.Tools.TrackReaders.Kml.LookAt')
  - [altitude](#P-Math-Tools-TrackReaders-Kml-LookAt-altitude 'Math.Tools.TrackReaders.Kml.LookAt.altitude')
  - [altitudeSpecified](#P-Math-Tools-TrackReaders-Kml-LookAt-altitudeSpecified 'Math.Tools.TrackReaders.Kml.LookAt.altitudeSpecified')
  - [heading](#P-Math-Tools-TrackReaders-Kml-LookAt-heading 'Math.Tools.TrackReaders.Kml.LookAt.heading')
  - [latitude](#P-Math-Tools-TrackReaders-Kml-LookAt-latitude 'Math.Tools.TrackReaders.Kml.LookAt.latitude')
  - [longitude](#P-Math-Tools-TrackReaders-Kml-LookAt-longitude 'Math.Tools.TrackReaders.Kml.LookAt.longitude')
  - [range](#P-Math-Tools-TrackReaders-Kml-LookAt-range 'Math.Tools.TrackReaders.Kml.LookAt.range')
  - [tilt](#P-Math-Tools-TrackReaders-Kml-LookAt-tilt 'Math.Tools.TrackReaders.Kml.LookAt.tilt')
- [MultiSportFolder_t](#T-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Extensions 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.Extensions')
  - [Folder](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Folder 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.Folder')
  - [MultisportActivityRef](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-MultisportActivityRef 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.MultisportActivityRef')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Name 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Notes 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.Notes')
  - [Week](#P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Week 'Math.Tools.TrackReaders.Tcx.MultiSportFolder_t.Week')
- [MultiSportSession_t](#T-Math-Tools-TrackReaders-Tcx-MultiSportSession_t 'Math.Tools.TrackReaders.Tcx.MultiSportSession_t')
  - [FirstSport](#P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-FirstSport 'Math.Tools.TrackReaders.Tcx.MultiSportSession_t.FirstSport')
  - [Id](#P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-Id 'Math.Tools.TrackReaders.Tcx.MultiSportSession_t.Id')
  - [NextSport](#P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-NextSport 'Math.Tools.TrackReaders.Tcx.MultiSportSession_t.NextSport')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-Notes 'Math.Tools.TrackReaders.Tcx.MultiSportSession_t.Notes')
- [NameKeyReference_t](#T-Math-Tools-TrackReaders-Tcx-NameKeyReference_t 'Math.Tools.TrackReaders.Tcx.NameKeyReference_t')
  - [Id](#P-Math-Tools-TrackReaders-Tcx-NameKeyReference_t-Id 'Math.Tools.TrackReaders.Tcx.NameKeyReference_t.Id')
- [NextSport_t](#T-Math-Tools-TrackReaders-Tcx-NextSport_t 'Math.Tools.TrackReaders.Tcx.NextSport_t')
  - [Activity](#P-Math-Tools-TrackReaders-Tcx-NextSport_t-Activity 'Math.Tools.TrackReaders.Tcx.NextSport_t.Activity')
  - [Transition](#P-Math-Tools-TrackReaders-Tcx-NextSport_t-Transition 'Math.Tools.TrackReaders.Tcx.NextSport_t.Transition')
- [None_t](#T-Math-Tools-TrackReaders-Tcx-None_t 'Math.Tools.TrackReaders.Tcx.None_t')
- [Pair](#T-Math-Tools-TrackReaders-Kml-Pair 'Math.Tools.TrackReaders.Kml.Pair')
  - [key](#P-Math-Tools-TrackReaders-Kml-Pair-key 'Math.Tools.TrackReaders.Kml.Pair.key')
  - [styleUrl](#P-Math-Tools-TrackReaders-Kml-Pair-styleUrl 'Math.Tools.TrackReaders.Kml.Pair.styleUrl')
- [Placemark](#T-Math-Tools-TrackReaders-Kml-Placemark 'Math.Tools.TrackReaders.Kml.Placemark')
  - [LineString](#P-Math-Tools-TrackReaders-Kml-Placemark-LineString 'Math.Tools.TrackReaders.Kml.Placemark.LineString')
  - [LookAt](#P-Math-Tools-TrackReaders-Kml-Placemark-LookAt 'Math.Tools.TrackReaders.Kml.Placemark.LookAt')
  - [Point](#P-Math-Tools-TrackReaders-Kml-Placemark-Point 'Math.Tools.TrackReaders.Kml.Placemark.Point')
  - [Polygon](#P-Math-Tools-TrackReaders-Kml-Placemark-Polygon 'Math.Tools.TrackReaders.Kml.Placemark.Polygon')
  - [description](#P-Math-Tools-TrackReaders-Kml-Placemark-description 'Math.Tools.TrackReaders.Kml.Placemark.description')
  - [name](#P-Math-Tools-TrackReaders-Kml-Placemark-name 'Math.Tools.TrackReaders.Kml.Placemark.name')
  - [styleUrl](#P-Math-Tools-TrackReaders-Kml-Placemark-styleUrl 'Math.Tools.TrackReaders.Kml.Placemark.styleUrl')
  - [visibility](#P-Math-Tools-TrackReaders-Kml-Placemark-visibility 'Math.Tools.TrackReaders.Kml.Placemark.visibility')
  - [visibilitySpecified](#P-Math-Tools-TrackReaders-Kml-Placemark-visibilitySpecified 'Math.Tools.TrackReaders.Kml.Placemark.visibilitySpecified')
- [Plan_t](#T-Math-Tools-TrackReaders-Tcx-Plan_t 'Math.Tools.TrackReaders.Tcx.Plan_t')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Plan_t-Extensions 'Math.Tools.TrackReaders.Tcx.Plan_t.Extensions')
  - [IntervalWorkout](#P-Math-Tools-TrackReaders-Tcx-Plan_t-IntervalWorkout 'Math.Tools.TrackReaders.Tcx.Plan_t.IntervalWorkout')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-Plan_t-Name 'Math.Tools.TrackReaders.Tcx.Plan_t.Name')
  - [Type](#P-Math-Tools-TrackReaders-Tcx-Plan_t-Type 'Math.Tools.TrackReaders.Tcx.Plan_t.Type')
- [Point](#T-Math-Tools-TrackReaders-Kml-Point 'Math.Tools.TrackReaders.Kml.Point')
  - [altitudeMode](#P-Math-Tools-TrackReaders-Kml-Point-altitudeMode 'Math.Tools.TrackReaders.Kml.Point.altitudeMode')
  - [altitudeModeSpecified](#P-Math-Tools-TrackReaders-Kml-Point-altitudeModeSpecified 'Math.Tools.TrackReaders.Kml.Point.altitudeModeSpecified')
  - [coordinates](#P-Math-Tools-TrackReaders-Kml-Point-coordinates 'Math.Tools.TrackReaders.Kml.Point.coordinates')
  - [extrude](#P-Math-Tools-TrackReaders-Kml-Point-extrude 'Math.Tools.TrackReaders.Kml.Point.extrude')
  - [extrudeSpecified](#P-Math-Tools-TrackReaders-Kml-Point-extrudeSpecified 'Math.Tools.TrackReaders.Kml.Point.extrudeSpecified')
- [PointLineKml](#T-Math-Tools-TrackReaders-PointLineKml 'Math.Tools.TrackReaders.PointLineKml')
  - [LastError](#P-Math-Tools-TrackReaders-PointLineKml-LastError 'Math.Tools.TrackReaders.PointLineKml.LastError')
  - [KMLDecode(fileName)](#M-Math-Tools-TrackReaders-PointLineKml-KMLDecode-System-String- 'Math.Tools.TrackReaders.PointLineKml.KMLDecode(System.String)')
  - [parseGeometryVal(tag_value)](#M-Math-Tools-TrackReaders-PointLineKml-parseGeometryVal-System-String- 'Math.Tools.TrackReaders.PointLineKml.parseGeometryVal(System.String)')
  - [parseLine(tag_value)](#M-Math-Tools-TrackReaders-PointLineKml-parseLine-System-String- 'Math.Tools.TrackReaders.PointLineKml.parseLine(System.String)')
  - [parsePoint(tag_value)](#M-Math-Tools-TrackReaders-PointLineKml-parsePoint-System-String- 'Math.Tools.TrackReaders.PointLineKml.parsePoint(System.String)')
  - [readKML(fileName)](#M-Math-Tools-TrackReaders-PointLineKml-readKML-System-String- 'Math.Tools.TrackReaders.PointLineKml.readKML(System.String)')
- [PolyStyle](#T-Math-Tools-TrackReaders-Kml-PolyStyle 'Math.Tools.TrackReaders.Kml.PolyStyle')
  - [color](#P-Math-Tools-TrackReaders-Kml-PolyStyle-color 'Math.Tools.TrackReaders.Kml.PolyStyle.color')
- [Polygon](#T-Math-Tools-TrackReaders-Kml-Polygon 'Math.Tools.TrackReaders.Kml.Polygon')
  - [altitudeMode](#P-Math-Tools-TrackReaders-Kml-Polygon-altitudeMode 'Math.Tools.TrackReaders.Kml.Polygon.altitudeMode')
  - [extrude](#P-Math-Tools-TrackReaders-Kml-Polygon-extrude 'Math.Tools.TrackReaders.Kml.Polygon.extrude')
  - [extrudeSpecified](#P-Math-Tools-TrackReaders-Kml-Polygon-extrudeSpecified 'Math.Tools.TrackReaders.Kml.Polygon.extrudeSpecified')
  - [innerBoundaryIs](#P-Math-Tools-TrackReaders-Kml-Polygon-innerBoundaryIs 'Math.Tools.TrackReaders.Kml.Polygon.innerBoundaryIs')
  - [outerBoundaryIs](#P-Math-Tools-TrackReaders-Kml-Polygon-outerBoundaryIs 'Math.Tools.TrackReaders.Kml.Polygon.outerBoundaryIs')
  - [tessellate](#P-Math-Tools-TrackReaders-Kml-Polygon-tessellate 'Math.Tools.TrackReaders.Kml.Polygon.tessellate')
  - [tessellateSpecified](#P-Math-Tools-TrackReaders-Kml-Polygon-tessellateSpecified 'Math.Tools.TrackReaders.Kml.Polygon.tessellateSpecified')
- [Position_t](#T-Math-Tools-TrackReaders-Tcx-Position_t 'Math.Tools.TrackReaders.Tcx.Position_t')
  - [LatitudeDegrees](#P-Math-Tools-TrackReaders-Tcx-Position_t-LatitudeDegrees 'Math.Tools.TrackReaders.Tcx.Position_t.LatitudeDegrees')
  - [LongitudeDegrees](#P-Math-Tools-TrackReaders-Tcx-Position_t-LongitudeDegrees 'Math.Tools.TrackReaders.Tcx.Position_t.LongitudeDegrees')
- [PredefinedHeartRateZone_t](#T-Math-Tools-TrackReaders-Tcx-PredefinedHeartRateZone_t 'Math.Tools.TrackReaders.Tcx.PredefinedHeartRateZone_t')
  - [Number](#P-Math-Tools-TrackReaders-Tcx-PredefinedHeartRateZone_t-Number 'Math.Tools.TrackReaders.Tcx.PredefinedHeartRateZone_t.Number')
- [PredefinedSpeedZone_t](#T-Math-Tools-TrackReaders-Tcx-PredefinedSpeedZone_t 'Math.Tools.TrackReaders.Tcx.PredefinedSpeedZone_t')
  - [Number](#P-Math-Tools-TrackReaders-Tcx-PredefinedSpeedZone_t-Number 'Math.Tools.TrackReaders.Tcx.PredefinedSpeedZone_t.Number')
- [QuickWorkout_t](#T-Math-Tools-TrackReaders-Tcx-QuickWorkout_t 'Math.Tools.TrackReaders.Tcx.QuickWorkout_t')
  - [DistanceMeters](#P-Math-Tools-TrackReaders-Tcx-QuickWorkout_t-DistanceMeters 'Math.Tools.TrackReaders.Tcx.QuickWorkout_t.DistanceMeters')
  - [TotalTimeSeconds](#P-Math-Tools-TrackReaders-Tcx-QuickWorkout_t-TotalTimeSeconds 'Math.Tools.TrackReaders.Tcx.QuickWorkout_t.TotalTimeSeconds')
- [Repeat_t](#T-Math-Tools-TrackReaders-Tcx-Repeat_t 'Math.Tools.TrackReaders.Tcx.Repeat_t')
  - [Child](#P-Math-Tools-TrackReaders-Tcx-Repeat_t-Child 'Math.Tools.TrackReaders.Tcx.Repeat_t.Child')
  - [Repetitions](#P-Math-Tools-TrackReaders-Tcx-Repeat_t-Repetitions 'Math.Tools.TrackReaders.Tcx.Repeat_t.Repetitions')
- [ScreenOverlay](#T-Math-Tools-TrackReaders-Kml-ScreenOverlay 'Math.Tools.TrackReaders.Kml.ScreenOverlay')
  - [Icon](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-Icon 'Math.Tools.TrackReaders.Kml.ScreenOverlay.Icon')
  - [description](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-description 'Math.Tools.TrackReaders.Kml.ScreenOverlay.description')
  - [name](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-name 'Math.Tools.TrackReaders.Kml.ScreenOverlay.name')
  - [overlayXY](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-overlayXY 'Math.Tools.TrackReaders.Kml.ScreenOverlay.overlayXY')
  - [rotationXY](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-rotationXY 'Math.Tools.TrackReaders.Kml.ScreenOverlay.rotationXY')
  - [screenXY](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-screenXY 'Math.Tools.TrackReaders.Kml.ScreenOverlay.screenXY')
  - [size](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-size 'Math.Tools.TrackReaders.Kml.ScreenOverlay.size')
  - [visibility](#P-Math-Tools-TrackReaders-Kml-ScreenOverlay-visibility 'Math.Tools.TrackReaders.Kml.ScreenOverlay.visibility')
- [SensorState_t](#T-Math-Tools-TrackReaders-Tcx-SensorState_t 'Math.Tools.TrackReaders.Tcx.SensorState_t')
  - [Absent](#F-Math-Tools-TrackReaders-Tcx-SensorState_t-Absent 'Math.Tools.TrackReaders.Tcx.SensorState_t.Absent')
  - [Present](#F-Math-Tools-TrackReaders-Tcx-SensorState_t-Present 'Math.Tools.TrackReaders.Tcx.SensorState_t.Present')
- [SpeedType_t](#T-Math-Tools-TrackReaders-Tcx-SpeedType_t 'Math.Tools.TrackReaders.Tcx.SpeedType_t')
  - [Pace](#F-Math-Tools-TrackReaders-Tcx-SpeedType_t-Pace 'Math.Tools.TrackReaders.Tcx.SpeedType_t.Pace')
  - [Speed](#F-Math-Tools-TrackReaders-Tcx-SpeedType_t-Speed 'Math.Tools.TrackReaders.Tcx.SpeedType_t.Speed')
- [Speed_t](#T-Math-Tools-TrackReaders-Tcx-Speed_t 'Math.Tools.TrackReaders.Tcx.Speed_t')
  - [SpeedZone](#P-Math-Tools-TrackReaders-Tcx-Speed_t-SpeedZone 'Math.Tools.TrackReaders.Tcx.Speed_t.SpeedZone')
- [SportType](#T-Math-Tools-TrackReaders-SportType 'Math.Tools.TrackReaders.SportType')
  - [Cycling](#F-Math-Tools-TrackReaders-SportType-Cycling 'Math.Tools.TrackReaders.SportType.Cycling')
  - [Running](#F-Math-Tools-TrackReaders-SportType-Running 'Math.Tools.TrackReaders.SportType.Running')
  - [Swimming](#F-Math-Tools-TrackReaders-SportType-Swimming 'Math.Tools.TrackReaders.SportType.Swimming')
  - [Unknown](#F-Math-Tools-TrackReaders-SportType-Unknown 'Math.Tools.TrackReaders.SportType.Unknown')
- [Sport_t](#T-Math-Tools-TrackReaders-Tcx-Sport_t 'Math.Tools.TrackReaders.Tcx.Sport_t')
  - [Biking](#F-Math-Tools-TrackReaders-Tcx-Sport_t-Biking 'Math.Tools.TrackReaders.Tcx.Sport_t.Biking')
  - [Other](#F-Math-Tools-TrackReaders-Tcx-Sport_t-Other 'Math.Tools.TrackReaders.Tcx.Sport_t.Other')
  - [Running](#F-Math-Tools-TrackReaders-Tcx-Sport_t-Running 'Math.Tools.TrackReaders.Tcx.Sport_t.Running')
- [Step_t](#T-Math-Tools-TrackReaders-Tcx-Step_t 'Math.Tools.TrackReaders.Tcx.Step_t')
  - [Duration](#P-Math-Tools-TrackReaders-Tcx-Step_t-Duration 'Math.Tools.TrackReaders.Tcx.Step_t.Duration')
  - [Intensity](#P-Math-Tools-TrackReaders-Tcx-Step_t-Intensity 'Math.Tools.TrackReaders.Tcx.Step_t.Intensity')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-Step_t-Name 'Math.Tools.TrackReaders.Tcx.Step_t.Name')
  - [Target](#P-Math-Tools-TrackReaders-Tcx-Step_t-Target 'Math.Tools.TrackReaders.Tcx.Step_t.Target')
- [Style](#T-Math-Tools-TrackReaders-Kml-Style 'Math.Tools.TrackReaders.Kml.Style')
  - [BalloonStyle](#P-Math-Tools-TrackReaders-Kml-Style-BalloonStyle 'Math.Tools.TrackReaders.Kml.Style.BalloonStyle')
  - [IconStyle](#P-Math-Tools-TrackReaders-Kml-Style-IconStyle 'Math.Tools.TrackReaders.Kml.Style.IconStyle')
  - [LineStyle](#P-Math-Tools-TrackReaders-Kml-Style-LineStyle 'Math.Tools.TrackReaders.Kml.Style.LineStyle')
  - [PolyStyle](#P-Math-Tools-TrackReaders-Kml-Style-PolyStyle 'Math.Tools.TrackReaders.Kml.Style.PolyStyle')
  - [id](#P-Math-Tools-TrackReaders-Kml-Style-id 'Math.Tools.TrackReaders.Kml.Style.id')
- [StyleMap](#T-Math-Tools-TrackReaders-Kml-StyleMap 'Math.Tools.TrackReaders.Kml.StyleMap')
  - [Pair](#P-Math-Tools-TrackReaders-Kml-StyleMap-Pair 'Math.Tools.TrackReaders.Kml.StyleMap.Pair')
  - [id](#P-Math-Tools-TrackReaders-Kml-StyleMap-id 'Math.Tools.TrackReaders.Kml.StyleMap.id')
- [Target_t](#T-Math-Tools-TrackReaders-Tcx-Target_t 'Math.Tools.TrackReaders.Tcx.Target_t')
- [TcxConverter](#T-Math-Tools-TrackReaders-TcxConverter 'Math.Tools.TrackReaders.TcxConverter')
  - [Convert(data)](#M-Math-Tools-TrackReaders-TcxConverter-Convert-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t- 'Math.Tools.TrackReaders.TcxConverter.Convert(Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t)')
- [Time_t](#T-Math-Tools-TrackReaders-Tcx-Time_t 'Math.Tools.TrackReaders.Tcx.Time_t')
  - [Seconds](#P-Math-Tools-TrackReaders-Tcx-Time_t-Seconds 'Math.Tools.TrackReaders.Tcx.Time_t.Seconds')
- [Track](#T-Math-Tools-TrackReaders-Track 'Math.Tools.TrackReaders.Track')
  - [Date](#P-Math-Tools-TrackReaders-Track-Date 'Math.Tools.TrackReaders.Track.Date')
  - [Name](#P-Math-Tools-TrackReaders-Track-Name 'Math.Tools.TrackReaders.Track.Name')
  - [SportType](#P-Math-Tools-TrackReaders-Track-SportType 'Math.Tools.TrackReaders.Track.SportType')
  - [TrackPoints](#P-Math-Tools-TrackReaders-Track-TrackPoints 'Math.Tools.TrackReaders.Track.TrackPoints')
  - [ElapsedSeconds()](#M-Math-Tools-TrackReaders-Track-ElapsedSeconds 'Math.Tools.TrackReaders.Track.ElapsedSeconds')
  - [GpsPoints()](#M-Math-Tools-TrackReaders-Track-GpsPoints 'Math.Tools.TrackReaders.Track.GpsPoints')
  - [HeartRates()](#M-Math-Tools-TrackReaders-Track-HeartRates 'Math.Tools.TrackReaders.Track.HeartRates')
  - [Seconds()](#M-Math-Tools-TrackReaders-Track-Seconds 'Math.Tools.TrackReaders.Track.Seconds')
  - [Times()](#M-Math-Tools-TrackReaders-Track-Times 'Math.Tools.TrackReaders.Track.Times')
- [TrackPoint](#T-Math-Tools-TrackReaders-TrackPoint 'Math.Tools.TrackReaders.TrackPoint')
  - [#ctor(latitude,longitude,elevation,distance,heartRate,time)](#M-Math-Tools-TrackReaders-TrackPoint-#ctor-System-Double,System-Double,System-Double,System-Double,System-Byte,System-DateTime- 'Math.Tools.TrackReaders.TrackPoint.#ctor(System.Double,System.Double,System.Double,System.Double,System.Byte,System.DateTime)')
  - [Distance](#P-Math-Tools-TrackReaders-TrackPoint-Distance 'Math.Tools.TrackReaders.TrackPoint.Distance')
  - [Elevation](#P-Math-Tools-TrackReaders-TrackPoint-Elevation 'Math.Tools.TrackReaders.TrackPoint.Elevation')
  - [Gps](#P-Math-Tools-TrackReaders-TrackPoint-Gps 'Math.Tools.TrackReaders.TrackPoint.Gps')
  - [HeartRate](#P-Math-Tools-TrackReaders-TrackPoint-HeartRate 'Math.Tools.TrackReaders.TrackPoint.HeartRate')
  - [Latitude](#P-Math-Tools-TrackReaders-TrackPoint-Latitude 'Math.Tools.TrackReaders.TrackPoint.Latitude')
  - [Longitude](#P-Math-Tools-TrackReaders-TrackPoint-Longitude 'Math.Tools.TrackReaders.TrackPoint.Longitude')
  - [Second](#P-Math-Tools-TrackReaders-TrackPoint-Second 'Math.Tools.TrackReaders.TrackPoint.Second')
  - [Time](#P-Math-Tools-TrackReaders-TrackPoint-Time 'Math.Tools.TrackReaders.TrackPoint.Time')
- [TrackPointExtension_t](#T-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t')
  - [Extensions](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-Extensions 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.Extensions')
  - [atemp](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-atemp 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.atemp')
  - [atempSpecified](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-atempSpecified 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.atempSpecified')
  - [cad](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-cad 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.cad')
  - [cadSpecified](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-cadSpecified 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.cadSpecified')
  - [depth](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-depth 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.depth')
  - [depthSpecified](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-depthSpecified 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.depthSpecified')
  - [hr](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-hr 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.hr')
  - [hrSpecified](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-hrSpecified 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.hrSpecified')
  - [wtemp](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-wtemp 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.wtemp')
  - [wtempSpecified](#P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-wtempSpecified 'Math.Tools.TrackReaders.Gpx.TrackPointExtension_t.wtempSpecified')
- [Trackpoint_t](#T-Math-Tools-TrackReaders-Tcx-Trackpoint_t 'Math.Tools.TrackReaders.Tcx.Trackpoint_t')
  - [AltitudeMeters](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-AltitudeMeters 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.AltitudeMeters')
  - [AltitudeMetersSpecified](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-AltitudeMetersSpecified 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.AltitudeMetersSpecified')
  - [Cadence](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Cadence 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.Cadence')
  - [CadenceSpecified](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-CadenceSpecified 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.CadenceSpecified')
  - [DistanceMeters](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-DistanceMeters 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.DistanceMeters')
  - [DistanceMetersSpecified](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-DistanceMetersSpecified 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.DistanceMetersSpecified')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Extensions 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.Extensions')
  - [HeartRateBpm](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-HeartRateBpm 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.HeartRateBpm')
  - [Position](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Position 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.Position')
  - [SensorState](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-SensorState 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.SensorState')
  - [SensorStateSpecified](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-SensorStateSpecified 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.SensorStateSpecified')
  - [Time](#P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Time 'Math.Tools.TrackReaders.Tcx.Trackpoint_t.Time')
- [TrainingCenterDatabase_t](#T-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t')
  - [Activities](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Activities 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Activities')
  - [Author](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Author 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Author')
  - [Courses](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Courses 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Courses')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Extensions 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Extensions')
  - [Folders](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Folders 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Folders')
  - [Workouts](#P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Workouts 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t.Workouts')
- [TrainingType_t](#T-Math-Tools-TrackReaders-Tcx-TrainingType_t 'Math.Tools.TrackReaders.Tcx.TrainingType_t')
  - [Course](#F-Math-Tools-TrackReaders-Tcx-TrainingType_t-Course 'Math.Tools.TrackReaders.Tcx.TrainingType_t.Course')
  - [Workout](#F-Math-Tools-TrackReaders-Tcx-TrainingType_t-Workout 'Math.Tools.TrackReaders.Tcx.TrainingType_t.Workout')
- [Training_t](#T-Math-Tools-TrackReaders-Tcx-Training_t 'Math.Tools.TrackReaders.Tcx.Training_t')
  - [Plan](#P-Math-Tools-TrackReaders-Tcx-Training_t-Plan 'Math.Tools.TrackReaders.Tcx.Training_t.Plan')
  - [QuickWorkoutResults](#P-Math-Tools-TrackReaders-Tcx-Training_t-QuickWorkoutResults 'Math.Tools.TrackReaders.Tcx.Training_t.QuickWorkoutResults')
  - [VirtualPartner](#P-Math-Tools-TrackReaders-Tcx-Training_t-VirtualPartner 'Math.Tools.TrackReaders.Tcx.Training_t.VirtualPartner')
- [TriggerMethod_t](#T-Math-Tools-TrackReaders-Tcx-TriggerMethod_t 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t')
  - [Distance](#F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Distance 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t.Distance')
  - [HeartRate](#F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-HeartRate 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t.HeartRate')
  - [Location](#F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Location 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t.Location')
  - [Manual](#F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Manual 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t.Manual')
  - [Time](#F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Time 'Math.Tools.TrackReaders.Tcx.TriggerMethod_t.Time')
- [UserInitiated_t](#T-Math-Tools-TrackReaders-Tcx-UserInitiated_t 'Math.Tools.TrackReaders.Tcx.UserInitiated_t')
- [Version_t](#T-Math-Tools-TrackReaders-Tcx-Version_t 'Math.Tools.TrackReaders.Tcx.Version_t')
  - [BuildMajor](#P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMajor 'Math.Tools.TrackReaders.Tcx.Version_t.BuildMajor')
  - [BuildMajorSpecified](#P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMajorSpecified 'Math.Tools.TrackReaders.Tcx.Version_t.BuildMajorSpecified')
  - [BuildMinor](#P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMinor 'Math.Tools.TrackReaders.Tcx.Version_t.BuildMinor')
  - [BuildMinorSpecified](#P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMinorSpecified 'Math.Tools.TrackReaders.Tcx.Version_t.BuildMinorSpecified')
  - [VersionMajor](#P-Math-Tools-TrackReaders-Tcx-Version_t-VersionMajor 'Math.Tools.TrackReaders.Tcx.Version_t.VersionMajor')
  - [VersionMinor](#P-Math-Tools-TrackReaders-Tcx-Version_t-VersionMinor 'Math.Tools.TrackReaders.Tcx.Version_t.VersionMinor')
- [Week_t](#T-Math-Tools-TrackReaders-Tcx-Week_t 'Math.Tools.TrackReaders.Tcx.Week_t')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-Week_t-Notes 'Math.Tools.TrackReaders.Tcx.Week_t.Notes')
  - [StartDay](#P-Math-Tools-TrackReaders-Tcx-Week_t-StartDay 'Math.Tools.TrackReaders.Tcx.Week_t.StartDay')
- [WorkoutFolder_t](#T-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t 'Math.Tools.TrackReaders.Tcx.WorkoutFolder_t')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Extensions 'Math.Tools.TrackReaders.Tcx.WorkoutFolder_t.Extensions')
  - [Folder](#P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Folder 'Math.Tools.TrackReaders.Tcx.WorkoutFolder_t.Folder')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Name 'Math.Tools.TrackReaders.Tcx.WorkoutFolder_t.Name')
  - [WorkoutNameRef](#P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-WorkoutNameRef 'Math.Tools.TrackReaders.Tcx.WorkoutFolder_t.WorkoutNameRef')
- [Workout_t](#T-Math-Tools-TrackReaders-Tcx-Workout_t 'Math.Tools.TrackReaders.Tcx.Workout_t')
  - [Creator](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Creator 'Math.Tools.TrackReaders.Tcx.Workout_t.Creator')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Extensions 'Math.Tools.TrackReaders.Tcx.Workout_t.Extensions')
  - [Name](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Name 'Math.Tools.TrackReaders.Tcx.Workout_t.Name')
  - [Notes](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Notes 'Math.Tools.TrackReaders.Tcx.Workout_t.Notes')
  - [ScheduledOn](#P-Math-Tools-TrackReaders-Tcx-Workout_t-ScheduledOn 'Math.Tools.TrackReaders.Tcx.Workout_t.ScheduledOn')
  - [Sport](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Sport 'Math.Tools.TrackReaders.Tcx.Workout_t.Sport')
  - [Step](#P-Math-Tools-TrackReaders-Tcx-Workout_t-Step 'Math.Tools.TrackReaders.Tcx.Workout_t.Step')
- [Workouts_t](#T-Math-Tools-TrackReaders-Tcx-Workouts_t 'Math.Tools.TrackReaders.Tcx.Workouts_t')
  - [Biking](#P-Math-Tools-TrackReaders-Tcx-Workouts_t-Biking 'Math.Tools.TrackReaders.Tcx.Workouts_t.Biking')
  - [Extensions](#P-Math-Tools-TrackReaders-Tcx-Workouts_t-Extensions 'Math.Tools.TrackReaders.Tcx.Workouts_t.Extensions')
  - [Other](#P-Math-Tools-TrackReaders-Tcx-Workouts_t-Other 'Math.Tools.TrackReaders.Tcx.Workouts_t.Other')
  - [Running](#P-Math-Tools-TrackReaders-Tcx-Workouts_t-Running 'Math.Tools.TrackReaders.Tcx.Workouts_t.Running')
- [Zone_t](#T-Math-Tools-TrackReaders-Tcx-Zone_t 'Math.Tools.TrackReaders.Tcx.Zone_t')
- [altitudeModeEnumType](#T-Math-Tools-TrackReaders-Kml-altitudeModeEnumType 'Math.Tools.TrackReaders.Kml.altitudeModeEnumType')
  - [absolute](#F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-absolute 'Math.Tools.TrackReaders.Kml.altitudeModeEnumType.absolute')
  - [clampToGround](#F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-clampToGround 'Math.Tools.TrackReaders.Kml.altitudeModeEnumType.clampToGround')
  - [relativeToGround](#F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-relativeToGround 'Math.Tools.TrackReaders.Kml.altitudeModeEnumType.relativeToGround')
- [colorModeEnumType](#T-Math-Tools-TrackReaders-Kml-colorModeEnumType 'Math.Tools.TrackReaders.Kml.colorModeEnumType')
  - [normal](#F-Math-Tools-TrackReaders-Kml-colorModeEnumType-normal 'Math.Tools.TrackReaders.Kml.colorModeEnumType.normal')
  - [random](#F-Math-Tools-TrackReaders-Kml-colorModeEnumType-random 'Math.Tools.TrackReaders.Kml.colorModeEnumType.random')
- [displayModeEnumType](#T-Math-Tools-TrackReaders-Kml-displayModeEnumType 'Math.Tools.TrackReaders.Kml.displayModeEnumType')
  - [default](#F-Math-Tools-TrackReaders-Kml-displayModeEnumType-default 'Math.Tools.TrackReaders.Kml.displayModeEnumType.default')
  - [hide](#F-Math-Tools-TrackReaders-Kml-displayModeEnumType-hide 'Math.Tools.TrackReaders.Kml.displayModeEnumType.hide')
- [gpx](#T-Math-Tools-TrackReaders-Gpx-gpx 'Math.Tools.TrackReaders.Gpx.gpx')
  - [creator](#P-Math-Tools-TrackReaders-Gpx-gpx-creator 'Math.Tools.TrackReaders.Gpx.gpx.creator')
  - [metadata](#P-Math-Tools-TrackReaders-Gpx-gpx-metadata 'Math.Tools.TrackReaders.Gpx.gpx.metadata')
  - [rte](#P-Math-Tools-TrackReaders-Gpx-gpx-rte 'Math.Tools.TrackReaders.Gpx.gpx.rte')
  - [trk](#P-Math-Tools-TrackReaders-Gpx-gpx-trk 'Math.Tools.TrackReaders.Gpx.gpx.trk')
  - [version](#P-Math-Tools-TrackReaders-Gpx-gpx-version 'Math.Tools.TrackReaders.Gpx.gpx.version')
- [gpxMetadata](#T-Math-Tools-TrackReaders-Gpx-gpxMetadata 'Math.Tools.TrackReaders.Gpx.gpxMetadata')
  - [author](#P-Math-Tools-TrackReaders-Gpx-gpxMetadata-author 'Math.Tools.TrackReaders.Gpx.gpxMetadata.author')
  - [link](#P-Math-Tools-TrackReaders-Gpx-gpxMetadata-link 'Math.Tools.TrackReaders.Gpx.gpxMetadata.link')
  - [time](#P-Math-Tools-TrackReaders-Gpx-gpxMetadata-time 'Math.Tools.TrackReaders.Gpx.gpxMetadata.time')
- [gpxMetadataAuthor](#T-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthor')
  - [email](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor-email 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthor.email')
  - [name](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor-name 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthor.name')
- [gpxMetadataAuthorEmail](#T-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthorEmail')
  - [domain](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail-domain 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthorEmail.domain')
  - [id](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail-id 'Math.Tools.TrackReaders.Gpx.gpxMetadataAuthorEmail.id')
- [gpxMetadataLink](#T-Math-Tools-TrackReaders-Gpx-gpxMetadataLink 'Math.Tools.TrackReaders.Gpx.gpxMetadataLink')
  - [href](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataLink-href 'Math.Tools.TrackReaders.Gpx.gpxMetadataLink.href')
  - [text](#P-Math-Tools-TrackReaders-Gpx-gpxMetadataLink-text 'Math.Tools.TrackReaders.Gpx.gpxMetadataLink.text')
- [gpxRte](#T-Math-Tools-TrackReaders-Gpx-gpxRte 'Math.Tools.TrackReaders.Gpx.gpxRte')
  - [cmt](#P-Math-Tools-TrackReaders-Gpx-gpxRte-cmt 'Math.Tools.TrackReaders.Gpx.gpxRte.cmt')
  - [desc](#P-Math-Tools-TrackReaders-Gpx-gpxRte-desc 'Math.Tools.TrackReaders.Gpx.gpxRte.desc')
  - [link](#P-Math-Tools-TrackReaders-Gpx-gpxRte-link 'Math.Tools.TrackReaders.Gpx.gpxRte.link')
  - [name](#P-Math-Tools-TrackReaders-Gpx-gpxRte-name 'Math.Tools.TrackReaders.Gpx.gpxRte.name')
  - [number](#P-Math-Tools-TrackReaders-Gpx-gpxRte-number 'Math.Tools.TrackReaders.Gpx.gpxRte.number')
  - [rtept](#P-Math-Tools-TrackReaders-Gpx-gpxRte-rtept 'Math.Tools.TrackReaders.Gpx.gpxRte.rtept')
  - [src](#P-Math-Tools-TrackReaders-Gpx-gpxRte-src 'Math.Tools.TrackReaders.Gpx.gpxRte.src')
  - [type](#P-Math-Tools-TrackReaders-Gpx-gpxRte-type 'Math.Tools.TrackReaders.Gpx.gpxRte.type')
- [gpxRteLink](#T-Math-Tools-TrackReaders-Gpx-gpxRteLink 'Math.Tools.TrackReaders.Gpx.gpxRteLink')
  - [href](#P-Math-Tools-TrackReaders-Gpx-gpxRteLink-href 'Math.Tools.TrackReaders.Gpx.gpxRteLink.href')
  - [text](#P-Math-Tools-TrackReaders-Gpx-gpxRteLink-text 'Math.Tools.TrackReaders.Gpx.gpxRteLink.text')
- [gpxRteRtept](#T-Math-Tools-TrackReaders-Gpx-gpxRteRtept 'Math.Tools.TrackReaders.Gpx.gpxRteRtept')
  - [ageofdgpsdata](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ageofdgpsdata 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.ageofdgpsdata')
  - [ageofdgpsdataSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ageofdgpsdataSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.ageofdgpsdataSpecified')
  - [cmt](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-cmt 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.cmt')
  - [desc](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-desc 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.desc')
  - [dgpsid](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-dgpsid 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.dgpsid')
  - [ele](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ele 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.ele')
  - [eleSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-eleSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.eleSpecified')
  - [fix](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-fix 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.fix')
  - [fixSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-fixSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.fixSpecified')
  - [geoidheight](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-geoidheight 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.geoidheight')
  - [geoidheightSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-geoidheightSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.geoidheightSpecified')
  - [hdop](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-hdop 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.hdop')
  - [hdopSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-hdopSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.hdopSpecified')
  - [lat](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-lat 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.lat')
  - [link](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-link 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.link')
  - [lon](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-lon 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.lon')
  - [magvar](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-magvar 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.magvar')
  - [magvarSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-magvarSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.magvarSpecified')
  - [name](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-name 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.name')
  - [pdop](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-pdop 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.pdop')
  - [pdopSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-pdopSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.pdopSpecified')
  - [sat](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-sat 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.sat')
  - [src](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-src 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.src')
  - [sym](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-sym 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.sym')
  - [time](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-time 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.time')
  - [timeSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-timeSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.timeSpecified')
  - [type](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-type 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.type')
  - [vdop](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-vdop 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.vdop')
  - [vdopSpecified](#P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-vdopSpecified 'Math.Tools.TrackReaders.Gpx.gpxRteRtept.vdopSpecified')
- [gpxRteRteptFix](#T-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix')
  - [Item2d](#F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-Item2d 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix.Item2d')
  - [Item3d](#F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-Item3d 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix.Item3d')
  - [dgps](#F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-dgps 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix.dgps')
  - [none](#F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-none 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix.none')
  - [pps](#F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-pps 'Math.Tools.TrackReaders.Gpx.gpxRteRteptFix.pps')
- [gpxRteRteptLink](#T-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink 'Math.Tools.TrackReaders.Gpx.gpxRteRteptLink')
  - [href](#P-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink-href 'Math.Tools.TrackReaders.Gpx.gpxRteRteptLink.href')
  - [text](#P-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink-text 'Math.Tools.TrackReaders.Gpx.gpxRteRteptLink.text')
- [gpxTrk](#T-Math-Tools-TrackReaders-Gpx-gpxTrk 'Math.Tools.TrackReaders.Gpx.gpxTrk')
  - [link](#P-Math-Tools-TrackReaders-Gpx-gpxTrk-link 'Math.Tools.TrackReaders.Gpx.gpxTrk.link')
  - [src](#P-Math-Tools-TrackReaders-Gpx-gpxTrk-src 'Math.Tools.TrackReaders.Gpx.gpxTrk.src')
  - [trkseg](#P-Math-Tools-TrackReaders-Gpx-gpxTrk-trkseg 'Math.Tools.TrackReaders.Gpx.gpxTrk.trkseg')
  - [type](#P-Math-Tools-TrackReaders-Gpx-gpxTrk-type 'Math.Tools.TrackReaders.Gpx.gpxTrk.type')
- [gpxTrkLink](#T-Math-Tools-TrackReaders-Gpx-gpxTrkLink 'Math.Tools.TrackReaders.Gpx.gpxTrkLink')
  - [href](#P-Math-Tools-TrackReaders-Gpx-gpxTrkLink-href 'Math.Tools.TrackReaders.Gpx.gpxTrkLink.href')
  - [text](#P-Math-Tools-TrackReaders-Gpx-gpxTrkLink-text 'Math.Tools.TrackReaders.Gpx.gpxTrkLink.text')
- [gpxTrkTrkpt](#T-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt')
  - [ele](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-ele 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt.ele')
  - [extensions](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-extensions 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt.extensions')
  - [lat](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-lat 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt.lat')
  - [lon](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-lon 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt.lon')
  - [time](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-time 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkpt.time')
- [gpxTrkTrkptExtensions](#T-Math-Tools-TrackReaders-Gpx-gpxTrkTrkptExtensions 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkptExtensions')
  - [TrackPointExtension](#P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkptExtensions-TrackPointExtension 'Math.Tools.TrackReaders.Gpx.gpxTrkTrkptExtensions.TrackPointExtension')
- [gridOriginEnumType](#T-Math-Tools-TrackReaders-Kml-gridOriginEnumType 'Math.Tools.TrackReaders.Kml.gridOriginEnumType')
  - [lowerLeft](#F-Math-Tools-TrackReaders-Kml-gridOriginEnumType-lowerLeft 'Math.Tools.TrackReaders.Kml.gridOriginEnumType.lowerLeft')
  - [upperLeft](#F-Math-Tools-TrackReaders-Kml-gridOriginEnumType-upperLeft 'Math.Tools.TrackReaders.Kml.gridOriginEnumType.upperLeft')
- [innerBoundaryIs](#T-Math-Tools-TrackReaders-Kml-innerBoundaryIs 'Math.Tools.TrackReaders.Kml.innerBoundaryIs')
  - [LinearRing](#P-Math-Tools-TrackReaders-Kml-innerBoundaryIs-LinearRing 'Math.Tools.TrackReaders.Kml.innerBoundaryIs.LinearRing')
- [itemIconStateEnumType](#T-Math-Tools-TrackReaders-Kml-itemIconStateEnumType 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType')
  - [closed](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-closed 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.closed')
  - [error](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-error 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.error')
  - [fetching0](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching0 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.fetching0')
  - [fetching1](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching1 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.fetching1')
  - [fetching2](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching2 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.fetching2')
  - [open](#F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-open 'Math.Tools.TrackReaders.Kml.itemIconStateEnumType.open')
- [kml](#T-Math-Tools-TrackReaders-Kml-kml 'Math.Tools.TrackReaders.Kml.kml')
  - [Document](#P-Math-Tools-TrackReaders-Kml-kml-Document 'Math.Tools.TrackReaders.Kml.kml.Document')
- [listItemTypeEnumType](#T-Math-Tools-TrackReaders-Kml-listItemTypeEnumType 'Math.Tools.TrackReaders.Kml.listItemTypeEnumType')
  - [check](#F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-check 'Math.Tools.TrackReaders.Kml.listItemTypeEnumType.check')
  - [checkHideChildren](#F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-checkHideChildren 'Math.Tools.TrackReaders.Kml.listItemTypeEnumType.checkHideChildren')
  - [checkOffOnly](#F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-checkOffOnly 'Math.Tools.TrackReaders.Kml.listItemTypeEnumType.checkOffOnly')
  - [radioFolder](#F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-radioFolder 'Math.Tools.TrackReaders.Kml.listItemTypeEnumType.radioFolder')
- [outerBoundaryIs](#T-Math-Tools-TrackReaders-Kml-outerBoundaryIs 'Math.Tools.TrackReaders.Kml.outerBoundaryIs')
  - [LinearRing](#P-Math-Tools-TrackReaders-Kml-outerBoundaryIs-LinearRing 'Math.Tools.TrackReaders.Kml.outerBoundaryIs.LinearRing')
- [refreshModeEnumType](#T-Math-Tools-TrackReaders-Kml-refreshModeEnumType 'Math.Tools.TrackReaders.Kml.refreshModeEnumType')
  - [onChange](#F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onChange 'Math.Tools.TrackReaders.Kml.refreshModeEnumType.onChange')
  - [onExpire](#F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onExpire 'Math.Tools.TrackReaders.Kml.refreshModeEnumType.onExpire')
  - [onInterval](#F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onInterval 'Math.Tools.TrackReaders.Kml.refreshModeEnumType.onInterval')
- [shapeEnumType](#T-Math-Tools-TrackReaders-Kml-shapeEnumType 'Math.Tools.TrackReaders.Kml.shapeEnumType')
  - [cylinder](#F-Math-Tools-TrackReaders-Kml-shapeEnumType-cylinder 'Math.Tools.TrackReaders.Kml.shapeEnumType.cylinder')
  - [rectangle](#F-Math-Tools-TrackReaders-Kml-shapeEnumType-rectangle 'Math.Tools.TrackReaders.Kml.shapeEnumType.rectangle')
  - [sphere](#F-Math-Tools-TrackReaders-Kml-shapeEnumType-sphere 'Math.Tools.TrackReaders.Kml.shapeEnumType.sphere')
- [styleStateEnumType](#T-Math-Tools-TrackReaders-Kml-styleStateEnumType 'Math.Tools.TrackReaders.Kml.styleStateEnumType')
  - [highlight](#F-Math-Tools-TrackReaders-Kml-styleStateEnumType-highlight 'Math.Tools.TrackReaders.Kml.styleStateEnumType.highlight')
  - [normal](#F-Math-Tools-TrackReaders-Kml-styleStateEnumType-normal 'Math.Tools.TrackReaders.Kml.styleStateEnumType.normal')
- [unitsEnumType](#T-Math-Tools-TrackReaders-Kml-unitsEnumType 'Math.Tools.TrackReaders.Kml.unitsEnumType')
  - [fraction](#F-Math-Tools-TrackReaders-Kml-unitsEnumType-fraction 'Math.Tools.TrackReaders.Kml.unitsEnumType.fraction')
  - [insetPixels](#F-Math-Tools-TrackReaders-Kml-unitsEnumType-insetPixels 'Math.Tools.TrackReaders.Kml.unitsEnumType.insetPixels')
  - [pixels](#F-Math-Tools-TrackReaders-Kml-unitsEnumType-pixels 'Math.Tools.TrackReaders.Kml.unitsEnumType.pixels')
- [vec2Type](#T-Math-Tools-TrackReaders-Kml-vec2Type 'Math.Tools.TrackReaders.Kml.vec2Type')
  - [x](#P-Math-Tools-TrackReaders-Kml-vec2Type-x 'Math.Tools.TrackReaders.Kml.vec2Type.x')
  - [xunits](#P-Math-Tools-TrackReaders-Kml-vec2Type-xunits 'Math.Tools.TrackReaders.Kml.vec2Type.xunits')
  - [y](#P-Math-Tools-TrackReaders-Kml-vec2Type-y 'Math.Tools.TrackReaders.Kml.vec2Type.y')
  - [yunits](#P-Math-Tools-TrackReaders-Kml-vec2Type-yunits 'Math.Tools.TrackReaders.Kml.vec2Type.yunits')
- [viewRefreshModeEnumType](#T-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType 'Math.Tools.TrackReaders.Kml.viewRefreshModeEnumType')
  - [never](#F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-never 'Math.Tools.TrackReaders.Kml.viewRefreshModeEnumType.never')
  - [onRegion](#F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onRegion 'Math.Tools.TrackReaders.Kml.viewRefreshModeEnumType.onRegion')
  - [onRequest](#F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onRequest 'Math.Tools.TrackReaders.Kml.viewRefreshModeEnumType.onRequest')
  - [onStop](#F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onStop 'Math.Tools.TrackReaders.Kml.viewRefreshModeEnumType.onStop')

<a name='T-Math-Tools-TrackReaders-Tcx-AbstractSource_t'></a>
## AbstractSource_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-AbstractSource_t-Name'></a>
### Name `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-AbstractStep_t'></a>
## AbstractStep_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-AbstractStep_t-StepId'></a>
### StepId `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-ActivityLap_t'></a>
## ActivityLap_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-AverageHeartRateBpm'></a>
### AverageHeartRateBpm `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Cadence'></a>
### Cadence `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-CadenceSpecified'></a>
### CadenceSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Calories'></a>
### Calories `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-DistanceMeters'></a>
### DistanceMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Intensity'></a>
### Intensity `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumHeartRateBpm'></a>
### MaximumHeartRateBpm `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumSpeed'></a>
### MaximumSpeed `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-MaximumSpeedSpecified'></a>
### MaximumSpeedSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-StartTime'></a>
### StartTime `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-TotalTimeSeconds'></a>
### TotalTimeSeconds `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-Track'></a>
### Track `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityLap_t-TriggerMethod'></a>
### TriggerMethod `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-ActivityList_t'></a>
## ActivityList_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityList_t-Activity'></a>
### Activity `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityList_t-MultiSportSession'></a>
### MultiSportSession `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-ActivityReference_t'></a>
## ActivityReference_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-ActivityReference_t-Id'></a>
### Id `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Activity_t'></a>
## Activity_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Creator'></a>
### Creator `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Id'></a>
### Id `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Lap'></a>
### Lap `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Sport'></a>
### Sport `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Activity_t-Training'></a>
### Training `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Application_t'></a>
## Application_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Application_t-Build'></a>
### Build `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Application_t-LangID'></a>
### LangID `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Application_t-PartNumber'></a>
### PartNumber `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-BalloonStyle'></a>
## BalloonStyle `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-BalloonStyle-text'></a>
### text `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-BuildType_t'></a>
## BuildType_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-BuildType_t-Alpha'></a>
### Alpha `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-BuildType_t-Beta'></a>
### Beta `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-BuildType_t-Internal'></a>
### Internal `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-BuildType_t-Release'></a>
### Release `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Build_t'></a>
## Build_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Build_t-Builder'></a>
### Builder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Build_t-Time'></a>
### Time `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Build_t-Type'></a>
### Type `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Build_t-TypeSpecified'></a>
### TypeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Build_t-Version'></a>
### Version `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Cadence_t'></a>
## Cadence_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Cadence_t-High'></a>
### High `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Cadence_t-Low'></a>
### Low `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CaloriesBurned_t'></a>
## CaloriesBurned_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CaloriesBurned_t-Calories'></a>
### Calories `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CourseFolder_t'></a>
## CourseFolder_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-CourseNameRef'></a>
### CourseNameRef `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Folder'></a>
### Folder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseFolder_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CourseLap_t'></a>
## CourseLap_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-AverageHeartRateBpm'></a>
### AverageHeartRateBpm `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginAltitudeMeters'></a>
### BeginAltitudeMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginAltitudeMetersSpecified'></a>
### BeginAltitudeMetersSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-BeginPosition'></a>
### BeginPosition `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Cadence'></a>
### Cadence `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-CadenceSpecified'></a>
### CadenceSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-DistanceMeters'></a>
### DistanceMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndAltitudeMeters'></a>
### EndAltitudeMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndAltitudeMetersSpecified'></a>
### EndAltitudeMetersSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-EndPosition'></a>
### EndPosition `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-Intensity'></a>
### Intensity `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-MaximumHeartRateBpm'></a>
### MaximumHeartRateBpm `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CourseLap_t-TotalTimeSeconds'></a>
### TotalTimeSeconds `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CoursePointType_t'></a>
## CoursePointType_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Danger'></a>
### Danger `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-FirstAid'></a>
### FirstAid `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Food'></a>
### Food `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Generic'></a>
### Generic `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-HorsCategory'></a>
### HorsCategory `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item1stCategory'></a>
### Item1stCategory `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item2ndCategory'></a>
### Item2ndCategory `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item3rdCategory'></a>
### Item3rdCategory `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Item4thCategory'></a>
### Item4thCategory `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Left'></a>
### Left `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Right'></a>
### Right `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Sprint'></a>
### Sprint `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Straight'></a>
### Straight `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Summit'></a>
### Summit `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Valley'></a>
### Valley `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-CoursePointType_t-Water'></a>
### Water `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CoursePoint_t'></a>
## CoursePoint_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-AltitudeMeters'></a>
### AltitudeMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-AltitudeMetersSpecified'></a>
### AltitudeMetersSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-PointType'></a>
### PointType `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Position'></a>
### Position `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CoursePoint_t-Time'></a>
### Time `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Course_t'></a>
## Course_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-CoursePoint'></a>
### CoursePoint `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Creator'></a>
### Creator `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Lap'></a>
### Lap `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Course_t-Track'></a>
### Track `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Courses_t'></a>
## Courses_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Courses_t-CourseFolder'></a>
### CourseFolder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Courses_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t'></a>
## CustomHeartRateZone_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t-High'></a>
### High `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CustomHeartRateZone_t-Low'></a>
### Low `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t'></a>
## CustomSpeedZone_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-HighInMetersPerSecond'></a>
### HighInMetersPerSecond `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-LowInMetersPerSecond'></a>
### LowInMetersPerSecond `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-CustomSpeedZone_t-ViewAs'></a>
### ViewAs `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Deserializer'></a>
## Deserializer `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

Reading activities containing GPS track by file, directory or string definition. Supported file format: TCX, GPX and KML.

<a name='M-Math-Tools-TrackReaders-Deserializer-Directory-System-String-'></a>
### Directory(path) `method`

##### Summary

Parses all TCX and GPX files of a given directory

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Path name |

<a name='M-Math-Tools-TrackReaders-Deserializer-File-System-String-'></a>
### File(filename) `method`

##### Summary

Reading and trying to parse either as TCX, GPX or KML based on its file type

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| filename | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | File name with *.tcx, *.gpx and *.kml file type |

<a name='M-Math-Tools-TrackReaders-Deserializer-String-System-String-'></a>
### String(input) `method`

##### Summary

Tryining to parse a string as a TCX, GPX or KML

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String containing a TCX, GPX or KML definition |

<a name='T-Math-Tools-TrackReaders-Tcx-Device_t'></a>
## Device_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Device_t-ProductID'></a>
### ProductID `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Device_t-UnitId'></a>
### UnitId `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Device_t-Version'></a>
### Version `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Distance_t'></a>
## Distance_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Distance_t-Meters'></a>
### Meters `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Document'></a>
## Document `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-Folder'></a>
### Folder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-LookAt'></a>
### LookAt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-Placemark'></a>
### Placemark `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-Style'></a>
### Style `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-StyleMap'></a>
### StyleMap `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-description'></a>
### description `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-open'></a>
### open `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-openSpecified'></a>
### openSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-visibility'></a>
### visibility `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Document-visibilitySpecified'></a>
### visibilitySpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Duration_t'></a>
## Duration_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-Extensions_t'></a>
## Extensions_t `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Extensions_t'></a>
## Extensions_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-Extensions_t-Any'></a>
### Any `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Extensions_t-Any'></a>
### Any `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-FirstSport_t'></a>
## FirstSport_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-FirstSport_t-Activity'></a>
### Activity `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Folder'></a>
## Folder `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-Document'></a>
### Document `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-Folder1'></a>
### Folder1 `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-GroundOverlay'></a>
### GroundOverlay `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-LookAt'></a>
### LookAt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-Placemark'></a>
### Placemark `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-ScreenOverlay'></a>
### ScreenOverlay `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-description'></a>
### description `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-styleUrl'></a>
### styleUrl `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-visibility'></a>
### visibility `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Folder-visibilitySpecified'></a>
### visibilitySpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Folders_t'></a>
## Folders_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Folders_t-Courses'></a>
### Courses `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Folders_t-History'></a>
### History `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Folders_t-Workouts'></a>
### Workouts `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-GpxConverter'></a>
## GpxConverter `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

GPX converter

<a name='M-Math-Tools-TrackReaders-GpxConverter-Convert-Math-Tools-TrackReaders-Gpx-gpx-'></a>
### Convert(data) `method`

##### Summary

Converts GPX data into abstract Track definition

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [Math.Tools.TrackReaders.Gpx.gpx](#T-Math-Tools-TrackReaders-Gpx-gpx 'Math.Tools.TrackReaders.Gpx.gpx') | GPX data |

<a name='T-Math-Tools-TrackReaders-Kml-GroundOverlay'></a>
## GroundOverlay `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-Icon'></a>
### Icon `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-LatLonBox'></a>
### LatLonBox `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-LookAt'></a>
### LookAt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-description'></a>
### description `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-GroundOverlay-visibility'></a>
### visibility `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRateAbove_t'></a>
## HeartRateAbove_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HeartRateAbove_t-HeartRate'></a>
### HeartRate `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRateAsPercentOfMax_t'></a>
## HeartRateAsPercentOfMax_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HeartRateAsPercentOfMax_t-Value'></a>
### Value `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRateBelow_t'></a>
## HeartRateBelow_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HeartRateBelow_t-HeartRate'></a>
### HeartRate `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRateInBeatsPerMinute_t'></a>
## HeartRateInBeatsPerMinute_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HeartRateInBeatsPerMinute_t-Value'></a>
### Value `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRateValue_t'></a>
## HeartRateValue_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HeartRate_t'></a>
## HeartRate_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HeartRate_t-HeartRateZone'></a>
### HeartRateZone `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-HistoryFolder_t'></a>
## HistoryFolder_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-ActivityRef'></a>
### ActivityRef `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Folder'></a>
### Folder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-HistoryFolder_t-Week'></a>
### Week `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-History_t'></a>
## History_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-History_t-Biking'></a>
### Biking `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-History_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-History_t-MultiSport'></a>
### MultiSport `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-History_t-Other'></a>
### Other `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-History_t-Running'></a>
### Running `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Icon'></a>
## Icon `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Icon-href'></a>
### href `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-IconStyle'></a>
## IconStyle `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-IconStyle-Icon'></a>
### Icon `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Intensity_t'></a>
## Intensity_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-Intensity_t-Active'></a>
### Active `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-Intensity_t-Resting'></a>
### Resting `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-KmlConverter'></a>
## KmlConverter `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

TCX Converter

<a name='M-Math-Tools-TrackReaders-KmlConverter-Convert-Math-Tools-TrackReaders-Kml-kml-'></a>
### Convert(data) `method`

##### Summary

Converts TXC data into abstract Track definition

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [Math.Tools.TrackReaders.Kml.kml](#T-Math-Tools-TrackReaders-Kml-kml 'Math.Tools.TrackReaders.Kml.kml') |  |

<a name='T-Math-Tools-TrackReaders-Kml-LatLonBox'></a>
## LatLonBox `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LatLonBox-east'></a>
### east `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LatLonBox-north'></a>
### north `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LatLonBox-rotation'></a>
### rotation `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LatLonBox-south'></a>
### south `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LatLonBox-west'></a>
### west `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-LineString'></a>
## LineString `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-altitudeMode'></a>
### altitudeMode `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-altitudeModeSpecified'></a>
### altitudeModeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-coordinates'></a>
### coordinates `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-extrude'></a>
### extrude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-extrudeSpecified'></a>
### extrudeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineString-tessellate'></a>
### tessellate `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-LineStyle'></a>
## LineStyle `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineStyle-color'></a>
### color `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineStyle-width'></a>
### width `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LineStyle-widthSpecified'></a>
### widthSpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-LinearRing'></a>
## LinearRing `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LinearRing-coordinates'></a>
### coordinates `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-LookAt'></a>
## LookAt `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-altitude'></a>
### altitude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-altitudeSpecified'></a>
### altitudeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-heading'></a>
### heading `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-latitude'></a>
### latitude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-longitude'></a>
### longitude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-range'></a>
### range `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-LookAt-tilt'></a>
### tilt `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t'></a>
## MultiSportFolder_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Folder'></a>
### Folder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-MultisportActivityRef'></a>
### MultisportActivityRef `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportFolder_t-Week'></a>
### Week `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-MultiSportSession_t'></a>
## MultiSportSession_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-FirstSport'></a>
### FirstSport `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-Id'></a>
### Id `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-NextSport'></a>
### NextSport `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-MultiSportSession_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-NameKeyReference_t'></a>
## NameKeyReference_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-NameKeyReference_t-Id'></a>
### Id `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-NextSport_t'></a>
## NextSport_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-NextSport_t-Activity'></a>
### Activity `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-NextSport_t-Transition'></a>
### Transition `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-None_t'></a>
## None_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Pair'></a>
## Pair `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Pair-key'></a>
### key `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Pair-styleUrl'></a>
### styleUrl `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Placemark'></a>
## Placemark `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-LineString'></a>
### LineString `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-LookAt'></a>
### LookAt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-Point'></a>
### Point `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-Polygon'></a>
### Polygon `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-description'></a>
### description `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-styleUrl'></a>
### styleUrl `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-visibility'></a>
### visibility `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Placemark-visibilitySpecified'></a>
### visibilitySpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Plan_t'></a>
## Plan_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Plan_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Plan_t-IntervalWorkout'></a>
### IntervalWorkout `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Plan_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Plan_t-Type'></a>
### Type `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Point'></a>
## Point `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Point-altitudeMode'></a>
### altitudeMode `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Point-altitudeModeSpecified'></a>
### altitudeModeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Point-coordinates'></a>
### coordinates `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Point-extrude'></a>
### extrude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Point-extrudeSpecified'></a>
### extrudeSpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-PointLineKml'></a>
## PointLineKml `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

PointLineKml

<a name='P-Math-Tools-TrackReaders-PointLineKml-LastError'></a>
### LastError `property`

##### Summary

Last returned error

<a name='M-Math-Tools-TrackReaders-PointLineKml-KMLDecode-System-String-'></a>
### KMLDecode(fileName) `method`

##### Summary

parse kml, fill Points and Lines collections

##### Returns

HashTable

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Full ABSOLUTE path to file. |

<a name='M-Math-Tools-TrackReaders-PointLineKml-parseGeometryVal-System-String-'></a>
### parseGeometryVal(tag_value) `method`

##### Summary

Parse selected geometry based on type

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag_value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Value of geometry element. |

<a name='M-Math-Tools-TrackReaders-PointLineKml-parseLine-System-String-'></a>
### parseLine(tag_value) `method`

##### Summary

If geometry is line select element values:
    COORDINATES - add lat and lan to List
      add list to HashTable Line

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag_value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Value of geometry element. |

<a name='M-Math-Tools-TrackReaders-PointLineKml-parsePoint-System-String-'></a>
### parsePoint(tag_value) `method`

##### Summary

If geometry is point select element values:
    COORDINATES - add lat and lan to HashTable Point

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tag_value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Value of geometry element. |

<a name='M-Math-Tools-TrackReaders-PointLineKml-readKML-System-String-'></a>
### readKML(fileName) `method`

##### Summary

Open kml, loop it and check for tags.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Full ABSOLUTE path to file. |

<a name='T-Math-Tools-TrackReaders-Kml-PolyStyle'></a>
## PolyStyle `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-PolyStyle-color'></a>
### color `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Polygon'></a>
## Polygon `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-altitudeMode'></a>
### altitudeMode `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-extrude'></a>
### extrude `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-extrudeSpecified'></a>
### extrudeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-innerBoundaryIs'></a>
### innerBoundaryIs `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-outerBoundaryIs'></a>
### outerBoundaryIs `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-tessellate'></a>
### tessellate `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Polygon-tessellateSpecified'></a>
### tessellateSpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Position_t'></a>
## Position_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Position_t-LatitudeDegrees'></a>
### LatitudeDegrees `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Position_t-LongitudeDegrees'></a>
### LongitudeDegrees `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-PredefinedHeartRateZone_t'></a>
## PredefinedHeartRateZone_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-PredefinedHeartRateZone_t-Number'></a>
### Number `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-PredefinedSpeedZone_t'></a>
## PredefinedSpeedZone_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-PredefinedSpeedZone_t-Number'></a>
### Number `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-QuickWorkout_t'></a>
## QuickWorkout_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-QuickWorkout_t-DistanceMeters'></a>
### DistanceMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-QuickWorkout_t-TotalTimeSeconds'></a>
### TotalTimeSeconds `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Repeat_t'></a>
## Repeat_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Repeat_t-Child'></a>
### Child `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Repeat_t-Repetitions'></a>
### Repetitions `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-ScreenOverlay'></a>
## ScreenOverlay `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-Icon'></a>
### Icon `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-description'></a>
### description `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-overlayXY'></a>
### overlayXY `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-rotationXY'></a>
### rotationXY `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-screenXY'></a>
### screenXY `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-size'></a>
### size `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-ScreenOverlay-visibility'></a>
### visibility `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-SensorState_t'></a>
## SensorState_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-SensorState_t-Absent'></a>
### Absent `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-SensorState_t-Present'></a>
### Present `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-SpeedType_t'></a>
## SpeedType_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-SpeedType_t-Pace'></a>
### Pace `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-SpeedType_t-Speed'></a>
### Speed `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Speed_t'></a>
## Speed_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Speed_t-SpeedZone'></a>
### SpeedZone `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-SportType'></a>
## SportType `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

Definition of activity types

<a name='F-Math-Tools-TrackReaders-SportType-Cycling'></a>
### Cycling `constants`

##### Summary

Cycling activity

<a name='F-Math-Tools-TrackReaders-SportType-Running'></a>
### Running `constants`

##### Summary

Running activity

<a name='F-Math-Tools-TrackReaders-SportType-Swimming'></a>
### Swimming `constants`

##### Summary

Swimming activity

<a name='F-Math-Tools-TrackReaders-SportType-Unknown'></a>
### Unknown `constants`

##### Summary

Some unknown or undefined activity

<a name='T-Math-Tools-TrackReaders-Tcx-Sport_t'></a>
## Sport_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-Sport_t-Biking'></a>
### Biking `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-Sport_t-Other'></a>
### Other `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-Sport_t-Running'></a>
### Running `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Step_t'></a>
## Step_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Step_t-Duration'></a>
### Duration `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Step_t-Intensity'></a>
### Intensity `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Step_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Step_t-Target'></a>
### Target `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-Style'></a>
## Style `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Style-BalloonStyle'></a>
### BalloonStyle `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Style-IconStyle'></a>
### IconStyle `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Style-LineStyle'></a>
### LineStyle `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Style-PolyStyle'></a>
### PolyStyle `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-Style-id'></a>
### id `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-StyleMap'></a>
## StyleMap `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-StyleMap-Pair'></a>
### Pair `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-StyleMap-id'></a>
### id `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Target_t'></a>
## Target_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-TcxConverter'></a>
## TcxConverter `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

TCX Converter

<a name='M-Math-Tools-TrackReaders-TcxConverter-Convert-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-'></a>
### Convert(data) `method`

##### Summary

Converts TXC data into abstract Track definition

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t](#T-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t 'Math.Tools.TrackReaders.Tcx.TrainingCenterDatabase_t') |  |

<a name='T-Math-Tools-TrackReaders-Tcx-Time_t'></a>
## Time_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Time_t-Seconds'></a>
### Seconds `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Track'></a>
## Track `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

Abstract GPS track definition including common data from GPX and TCX

<a name='P-Math-Tools-TrackReaders-Track-Date'></a>
### Date `property`

##### Summary

Start time and date of activity

<a name='P-Math-Tools-TrackReaders-Track-Name'></a>
### Name `property`

##### Summary

Name of the activity

<a name='P-Math-Tools-TrackReaders-Track-SportType'></a>
### SportType `property`

##### Summary

Type of activity

<a name='P-Math-Tools-TrackReaders-Track-TrackPoints'></a>
### TrackPoints `property`

##### Summary

List of track points

<a name='M-Math-Tools-TrackReaders-Track-ElapsedSeconds'></a>
### ElapsedSeconds() `method`

##### Summary

Returns list of elapsed seconds since start

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Tools-TrackReaders-Track-GpsPoints'></a>
### GpsPoints() `method`

##### Summary

Returns list of GpsPoints

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Tools-TrackReaders-Track-HeartRates'></a>
### HeartRates() `method`

##### Summary

Returns list of heart beats

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Tools-TrackReaders-Track-Seconds'></a>
### Seconds() `method`

##### Summary

Returns list of date and time in seconds

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Math-Tools-TrackReaders-Track-Times'></a>
### Times() `method`

##### Summary

Returns time and date of each track point

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Math-Tools-TrackReaders-TrackPoint'></a>
## TrackPoint `type`

##### Namespace

Math.Tools.TrackReaders

##### Summary

Abstract definition of a track point of an activity: GPS, elevation, distance, heart rate and time

<a name='M-Math-Tools-TrackReaders-TrackPoint-#ctor-System-Double,System-Double,System-Double,System-Double,System-Byte,System-DateTime-'></a>
### #ctor(latitude,longitude,elevation,distance,heartRate,time) `constructor`

##### Summary

A track point

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| latitude | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Latitude [deg] |
| longitude | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Longitude [deg] |
| elevation | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Elevation [m] |
| distance | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | Distance [m] |
| heartRate | [System.Byte](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte 'System.Byte') | Heart rate [byte] |
| time | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | Time and date |

<a name='P-Math-Tools-TrackReaders-TrackPoint-Distance'></a>
### Distance `property`

##### Summary

Returns distance [m]

<a name='P-Math-Tools-TrackReaders-TrackPoint-Elevation'></a>
### Elevation `property`

##### Summary

Returns elevation [m]

<a name='P-Math-Tools-TrackReaders-TrackPoint-Gps'></a>
### Gps `property`

##### Summary

Returns GpsPoint

<a name='P-Math-Tools-TrackReaders-TrackPoint-HeartRate'></a>
### HeartRate `property`

##### Summary

Returns heart beat

<a name='P-Math-Tools-TrackReaders-TrackPoint-Latitude'></a>
### Latitude `property`

##### Summary

Returns latitude [deg]

<a name='P-Math-Tools-TrackReaders-TrackPoint-Longitude'></a>
### Longitude `property`

##### Summary

Returns Longitude [deg]

<a name='P-Math-Tools-TrackReaders-TrackPoint-Second'></a>
### Second `property`

##### Summary

Returns seconds of date and time of current point

<a name='P-Math-Tools-TrackReaders-TrackPoint-Time'></a>
### Time `property`

##### Summary

Returns date and time of current point

<a name='T-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t'></a>
## TrackPointExtension_t `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-atemp'></a>
### atemp `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-atempSpecified'></a>
### atempSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-cad'></a>
### cad `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-cadSpecified'></a>
### cadSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-depth'></a>
### depth `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-depthSpecified'></a>
### depthSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-hr'></a>
### hr `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-hrSpecified'></a>
### hrSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-wtemp'></a>
### wtemp `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-TrackPointExtension_t-wtempSpecified'></a>
### wtempSpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Trackpoint_t'></a>
## Trackpoint_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-AltitudeMeters'></a>
### AltitudeMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-AltitudeMetersSpecified'></a>
### AltitudeMetersSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Cadence'></a>
### Cadence `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-CadenceSpecified'></a>
### CadenceSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-DistanceMeters'></a>
### DistanceMeters `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-DistanceMetersSpecified'></a>
### DistanceMetersSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-HeartRateBpm'></a>
### HeartRateBpm `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Position'></a>
### Position `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-SensorState'></a>
### SensorState `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-SensorStateSpecified'></a>
### SensorStateSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Trackpoint_t-Time'></a>
### Time `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t'></a>
## TrainingCenterDatabase_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Activities'></a>
### Activities `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Author'></a>
### Author `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Courses'></a>
### Courses `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Folders'></a>
### Folders `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-TrainingCenterDatabase_t-Workouts'></a>
### Workouts `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-TrainingType_t'></a>
## TrainingType_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TrainingType_t-Course'></a>
### Course `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TrainingType_t-Workout'></a>
### Workout `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Training_t'></a>
## Training_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Training_t-Plan'></a>
### Plan `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Training_t-QuickWorkoutResults'></a>
### QuickWorkoutResults `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Training_t-VirtualPartner'></a>
### VirtualPartner `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-TriggerMethod_t'></a>
## TriggerMethod_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Distance'></a>
### Distance `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-HeartRate'></a>
### HeartRate `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Location'></a>
### Location `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Manual'></a>
### Manual `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Tcx-TriggerMethod_t-Time'></a>
### Time `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-UserInitiated_t'></a>
## UserInitiated_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Version_t'></a>
## Version_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMajor'></a>
### BuildMajor `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMajorSpecified'></a>
### BuildMajorSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMinor'></a>
### BuildMinor `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-BuildMinorSpecified'></a>
### BuildMinorSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-VersionMajor'></a>
### VersionMajor `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Version_t-VersionMinor'></a>
### VersionMinor `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Week_t'></a>
## Week_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Week_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Week_t-StartDay'></a>
### StartDay `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t'></a>
## WorkoutFolder_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Folder'></a>
### Folder `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-WorkoutFolder_t-WorkoutNameRef'></a>
### WorkoutNameRef `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Workout_t'></a>
## Workout_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Creator'></a>
### Creator `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Name'></a>
### Name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Notes'></a>
### Notes `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-ScheduledOn'></a>
### ScheduledOn `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Sport'></a>
### Sport `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workout_t-Step'></a>
### Step `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Workouts_t'></a>
## Workouts_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workouts_t-Biking'></a>
### Biking `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workouts_t-Extensions'></a>
### Extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workouts_t-Other'></a>
### Other `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Tcx-Workouts_t-Running'></a>
### Running `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Tcx-Zone_t'></a>
## Zone_t `type`

##### Namespace

Math.Tools.TrackReaders.Tcx

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-altitudeModeEnumType'></a>
## altitudeModeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-absolute'></a>
### absolute `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-clampToGround'></a>
### clampToGround `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-altitudeModeEnumType-relativeToGround'></a>
### relativeToGround `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-colorModeEnumType'></a>
## colorModeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-colorModeEnumType-normal'></a>
### normal `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-colorModeEnumType-random'></a>
### random `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-displayModeEnumType'></a>
## displayModeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-displayModeEnumType-default'></a>
### default `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-displayModeEnumType-hide'></a>
### hide `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpx'></a>
## gpx `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpx-creator'></a>
### creator `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpx-metadata'></a>
### metadata `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpx-rte'></a>
### rte `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpx-trk'></a>
### trk `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpx-version'></a>
### version `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxMetadata'></a>
## gpxMetadata `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadata-author'></a>
### author `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadata-link'></a>
### link `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadata-time'></a>
### time `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor'></a>
## gpxMetadataAuthor `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor-email'></a>
### email `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthor-name'></a>
### name `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail'></a>
## gpxMetadataAuthorEmail `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail-domain'></a>
### domain `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataAuthorEmail-id'></a>
### id `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxMetadataLink'></a>
## gpxMetadataLink `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataLink-href'></a>
### href `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxMetadataLink-text'></a>
### text `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxRte'></a>
## gpxRte `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-cmt'></a>
### cmt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-desc'></a>
### desc `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-link'></a>
### link `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-number'></a>
### number `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-rtept'></a>
### rtept `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-src'></a>
### src `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRte-type'></a>
### type `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxRteLink'></a>
## gpxRteLink `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteLink-href'></a>
### href `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteLink-text'></a>
### text `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxRteRtept'></a>
## gpxRteRtept `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ageofdgpsdata'></a>
### ageofdgpsdata `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ageofdgpsdataSpecified'></a>
### ageofdgpsdataSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-cmt'></a>
### cmt `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-desc'></a>
### desc `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-dgpsid'></a>
### dgpsid `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-ele'></a>
### ele `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-eleSpecified'></a>
### eleSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-fix'></a>
### fix `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-fixSpecified'></a>
### fixSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-geoidheight'></a>
### geoidheight `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-geoidheightSpecified'></a>
### geoidheightSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-hdop'></a>
### hdop `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-hdopSpecified'></a>
### hdopSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-lat'></a>
### lat `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-link'></a>
### link `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-lon'></a>
### lon `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-magvar'></a>
### magvar `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-magvarSpecified'></a>
### magvarSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-name'></a>
### name `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-pdop'></a>
### pdop `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-pdopSpecified'></a>
### pdopSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-sat'></a>
### sat `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-src'></a>
### src `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-sym'></a>
### sym `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-time'></a>
### time `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-timeSpecified'></a>
### timeSpecified `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-type'></a>
### type `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-vdop'></a>
### vdop `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRtept-vdopSpecified'></a>
### vdopSpecified `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix'></a>
## gpxRteRteptFix `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-Item2d'></a>
### Item2d `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-Item3d'></a>
### Item3d `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-dgps'></a>
### dgps `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-none'></a>
### none `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Gpx-gpxRteRteptFix-pps'></a>
### pps `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink'></a>
## gpxRteRteptLink `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink-href'></a>
### href `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxRteRteptLink-text'></a>
### text `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxTrk'></a>
## gpxTrk `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrk-link'></a>
### link `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrk-src'></a>
### src `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrk-trkseg'></a>
### trkseg `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrk-type'></a>
### type `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxTrkLink'></a>
## gpxTrkLink `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkLink-href'></a>
### href `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkLink-text'></a>
### text `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt'></a>
## gpxTrkTrkpt `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-ele'></a>
### ele `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-extensions'></a>
### extensions `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-lat'></a>
### lat `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-lon'></a>
### lon `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkpt-time'></a>
### time `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Gpx-gpxTrkTrkptExtensions'></a>
## gpxTrkTrkptExtensions `type`

##### Namespace

Math.Tools.TrackReaders.Gpx

##### Remarks



<a name='P-Math-Tools-TrackReaders-Gpx-gpxTrkTrkptExtensions-TrackPointExtension'></a>
### TrackPointExtension `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-gridOriginEnumType'></a>
## gridOriginEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-gridOriginEnumType-lowerLeft'></a>
### lowerLeft `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-gridOriginEnumType-upperLeft'></a>
### upperLeft `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-innerBoundaryIs'></a>
## innerBoundaryIs `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-innerBoundaryIs-LinearRing'></a>
### LinearRing `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-itemIconStateEnumType'></a>
## itemIconStateEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-closed'></a>
### closed `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-error'></a>
### error `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching0'></a>
### fetching0 `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching1'></a>
### fetching1 `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-fetching2'></a>
### fetching2 `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-itemIconStateEnumType-open'></a>
### open `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-kml'></a>
## kml `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-kml-Document'></a>
### Document `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-listItemTypeEnumType'></a>
## listItemTypeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-check'></a>
### check `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-checkHideChildren'></a>
### checkHideChildren `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-checkOffOnly'></a>
### checkOffOnly `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-listItemTypeEnumType-radioFolder'></a>
### radioFolder `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-outerBoundaryIs'></a>
## outerBoundaryIs `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-outerBoundaryIs-LinearRing'></a>
### LinearRing `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-refreshModeEnumType'></a>
## refreshModeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onChange'></a>
### onChange `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onExpire'></a>
### onExpire `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-refreshModeEnumType-onInterval'></a>
### onInterval `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-shapeEnumType'></a>
## shapeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-shapeEnumType-cylinder'></a>
### cylinder `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-shapeEnumType-rectangle'></a>
### rectangle `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-shapeEnumType-sphere'></a>
### sphere `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-styleStateEnumType'></a>
## styleStateEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-styleStateEnumType-highlight'></a>
### highlight `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-styleStateEnumType-normal'></a>
### normal `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-unitsEnumType'></a>
## unitsEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-unitsEnumType-fraction'></a>
### fraction `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-unitsEnumType-insetPixels'></a>
### insetPixels `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-unitsEnumType-pixels'></a>
### pixels `constants`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-vec2Type'></a>
## vec2Type `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-vec2Type-x'></a>
### x `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-vec2Type-xunits'></a>
### xunits `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-vec2Type-y'></a>
### y `property`

##### Remarks



<a name='P-Math-Tools-TrackReaders-Kml-vec2Type-yunits'></a>
### yunits `property`

##### Remarks



<a name='T-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType'></a>
## viewRefreshModeEnumType `type`

##### Namespace

Math.Tools.TrackReaders.Kml

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-never'></a>
### never `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onRegion'></a>
### onRegion `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onRequest'></a>
### onRequest `constants`

##### Remarks



<a name='F-Math-Tools-TrackReaders-Kml-viewRefreshModeEnumType-onStop'></a>
### onStop `constants`

##### Remarks


