import React, { useState } from "react";
import { useParams } from "react-router-dom";
import ReactSelect from "react-select";
import { addUserToProcedure, removeUserFromProcedure } from "../../../api/api";

const PlanProcedureItem = ({ procedure, users, selectedUsersList }) => {
  let { id } = useParams();
  const [selectedUsers, setSelectedUsers] = useState(selectedUsersList);

  const handleAssignUserToProcedure = (e) => {
    // TODO: Remove console.log and add missing logic

    if (selectedUsers === null) {
      addUserToProcedure(e[0].value, procedure.procedureId, parseInt(id)).then(
        (res) => {
          setSelectedUsers([
            { label: e[0].label, value: e[0].value, userMappingId: res },
          ]);
        }
      );
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
          parseInt(id)
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
