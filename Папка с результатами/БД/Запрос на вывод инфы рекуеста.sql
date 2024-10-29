SELECT        Request.RequestId, Request.RequestDate, Equipment.EquipmentName, Problem.ProblemName, [User].UserFullName AS Client, Request.RequestDescription,
			  Status.StatusName, [User1].UserFullName AS [Master], Request.RequestTime, 
                         Priority.PriorityName, Stage.StageName, Request.RequestComment
FROM            Equipment INNER JOIN
                         Request ON Equipment.EquipmentID = Request.RequestEquipmentId INNER JOIN
                         Priority ON Request.RequestPriorityId = Priority.PriorityID INNER JOIN
                         Problem ON Request.RequestProblemId = Problem.ProblemID INNER JOIN
                         Stage ON Request.RequestStageId = Stage.StageID INNER JOIN
                         Status ON Request.RequestStatusId = Status.StatusID INNER JOIN
                         [User] ON Request.RequestUserId = [User].UserId INNER JOIN
						 [User] AS User1 ON Request.RequestMasterId = [User1].UserId