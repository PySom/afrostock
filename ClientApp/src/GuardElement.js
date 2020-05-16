import React from 'react';
import ls from "./sideEffects/local/ourLocalStorage";

export default function GuardElement({ whois, children }) {
    //get user from store
    const user = ls.getUserInLs();
    const checkIfAllowed = (who, role) => {
        if (Array.isArray(who)) {
            return who.includes(role)
        }
        else return who === role
    }
    return (user && checkIfAllowed(whois, user.role) && (
        <>
            {children}
        </>
    )
}
