import { useSiteStore } from "../store/siteStore";

export const UserInRole = (role: string[] | string): boolean => {
  if (Array.isArray(role)) {
    for (let i = 0; i < role.length; i++) {
      for (let r = 0; r < useSiteStore().roles.length; r++) {
        if (useSiteStore().roles[r] == role[i]) return true;
      }
    }
  } else {
    for (let r = 0; r < useSiteStore().roles.length; r++) {
      if (useSiteStore().roles[r] == role) return true;
    }
  }

  return false;
};

export default UserInRole;
