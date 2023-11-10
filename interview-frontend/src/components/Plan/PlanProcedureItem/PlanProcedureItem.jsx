import React, { useEffect, useState } from "react";
import ReactSelect from "react-select";
import {
  addUserToProcedure,
  getUserMappingList,
  removeUserFromProcedure,
} from "../../../api/api";

const PlanProcedureItem = ({ procedure, users }) => {
  const url = window.location.href.split("/");
  const planId = url[url.length - 1];
  useEffect(() => {
    getUserMappingList(procedure.procedureId, planId).then((usersList) => {
      if (usersList.length > 0) {
        const finalList = [];
        usersList.map((each) => {
          const user = users.find((p) => p.value === each.userId);
          finalList.push({
            label: user.label,
            value: each.userId,
            userMappingId: each.userMappingId,
          });
        });
        setSelectedUsers(finalList);
      }
    });
  }, []);
  const [selectedUsers, setSelectedUsers] = useState(null);

  const handleAssignUserToProcedure = (e) => {
    // TODO: Remove console.log and add missing logic

    if (selectedUsers === null) {
      addUserToProcedure(
        e[0].value,
        procedure.procedureId,
        parseInt(planId)
      ).then((res) => {
        setSelectedUsers([
          { label: e[0].label, value: e[0].value, userMappingId: res },
        ]);
      });
    } else {
      if (selectedUsers.length > e.length) {
        const removedUser = selectedUsers.filter(
          (user1) => !e.some((user2) => user1.value === user2.value)
        );
        if (removedUser.length > 1) {
          removedUser.forEach(async (each) => {
            await removeUserFromProcedure(each.userMappingId);
            setSelectedUsers(e);
          });
        } else {
          removeUserFromProcedure(removedUser[0].userMappingId).then((res) => {
            const filteredArr = selectedUsers.filter(
              (r) => r.userMappingId !== res
            );
            setSelectedUsers(filteredArr);
          });
        }
      } else {
        const addUser = e.filter(
          (user1) => !selectedUsers.some((user2) => user1.value === user2.value)
        );
        addUserToProcedure(
          addUser[0].value,
          procedure.procedureId,
          parseInt(planId)
        ).then((res) => {
          setSelectedUsers((prevProps) => [
            ...prevProps,
            {
              label: addUser[0].label,
              value: addUser[0].value,
              userMappingId: res,
            },
          ]);
        });
      }
    }
  };

  return (
    <div className="py-2">
      <div>{procedure.procedureTitle}</div>
      <ReactSelect
        className="mt-2"
        placeholder="Select User to Assign"
        isMulti={true}
        options={users}
        value={selectedUsers}
        onChange={(e) => handleAssignUserToProcedure(e)}
      />
    </div>
  );
};

export default PlanProcedureItem;
